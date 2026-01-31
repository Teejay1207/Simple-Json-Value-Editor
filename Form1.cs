using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Windows.Forms;

namespace Simple_Json_Value_Editor
{
    public partial class Form1 : Form
    {
        private JsonNode _rootNode;
        private string _currentFilePath;

        public Form1()
        {
            InitializeComponent();
        }

        // FILE → Open
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) != DialogResult.OK)
                return;

            try
            {
                var jsonText = File.ReadAllText(openFileDialog1.FileName);
                _rootNode = JsonNode.Parse(jsonText, new JsonNodeOptions
                {
                    PropertyNameCaseInsensitive = false
                });

                _currentFilePath = openFileDialog1.FileName;
                Text = $"Simple Json Value Editor - {_currentFilePath}";

                PopulateTree();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Failed to open JSON:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // FILE → Save
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_rootNode == null)
                return;

            if (string.IsNullOrWhiteSpace(_currentFilePath))
            {
                saveAsToolStripMenuItem_Click(sender, e);
                return;
            }

            SaveJson(_currentFilePath);
        }

        // FILE → Save As
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_rootNode == null)
                return;

            if (!string.IsNullOrWhiteSpace(_currentFilePath))
            {
                saveFileDialog1.InitialDirectory = Path.GetDirectoryName(_currentFilePath);
                saveFileDialog1.FileName = Path.GetFileName(_currentFilePath);
            }

            if (saveFileDialog1.ShowDialog(this) != DialogResult.OK)
                return;

            SaveJson(saveFileDialog1.FileName);
            _currentFilePath = saveFileDialog1.FileName;
            Text = $"Simple Json Value Editor - {_currentFilePath}";
        }

        private void SaveJson(string path)
        {
            try
            {
                var json = _rootNode.ToJsonString(new JsonSerializerOptions
                {
                    WriteIndented = true
                });
                File.WriteAllText(path, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Failed to save JSON:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Build the TreeView from the JsonNode
        private void PopulateTree()
        {
            treeViewJson.BeginUpdate();
            treeViewJson.Nodes.Clear();

            if (_rootNode != null)
            {
                var rootNode = new TreeNode("(root)")
                {
                    Tag = new JsonNodeTag(_rootNode, null, null, null)
                };
                treeViewJson.Nodes.Add(rootNode);
                BuildChildren(rootNode, _rootNode);
                rootNode.Expand();
            }

            treeViewJson.EndUpdate();
            lblPath.Text = "Select a value node on the left.";
            txtValue.Clear();
            txtValue.Visible = true;
            chkValue.Visible = false;
        }

        private void BuildChildren(TreeNode treeNode, JsonNode jsonNode)
        {
            if (jsonNode is JsonObject obj)
            {
                foreach (var kvp in obj)
                {
                    var child = new TreeNode(kvp.Key)
                    {
                        Tag = new JsonNodeTag(kvp.Value, obj, kvp.Key, null)
                    };
                    treeNode.Nodes.Add(child);
                    if (kvp.Value is JsonObject || kvp.Value is JsonArray)
                    {
                        BuildChildren(child, kvp.Value);
                    }
                }
            }
            else if (jsonNode is JsonArray array)
            {
                for (int i = 0; i < array.Count; i++)
                {
                    var child = new TreeNode($"[{i}]")
                    {
                        Tag = new JsonNodeTag(array[i], array, null, i)
                    };
                    treeNode.Nodes.Add(child);
                    if (array[i] is JsonObject || array[i] is JsonArray)
                    {
                        BuildChildren(child, array[i]);
                    }
                }
            }
        }

        private static bool IsValueNode(JsonNode node)
        {
            return node is JsonValue;
        }

        private string GetJsonPath(TreeNode node)
        {
            var parts = node
                .FullPath
                .Split(new[] { treeViewJson.PathSeparator }, StringSplitOptions.None);

            return string.Join(".", parts.Skip(1));
        }

        // FIXED: Safe pattern matching for tag
        private void treeViewJson_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var tempTag = e.Node?.Tag as JsonNodeTag;
            if (tempTag == null || tempTag.Node == null)
            {
                lblPath.Text = "No node selected.";
                txtValue.Clear();
                txtValue.Visible = true;
                chkValue.Visible = false;
                txtValue.Enabled = false;
                chkValue.Enabled = false;
                btnApply.Enabled = false;
                return;
            }
            var tag = tempTag;

            lblPath.Text = GetJsonPath(e.Node);
            txtValue.Visible = true;
            chkValue.Visible = false;

            if (IsValueNode(tag.Node))
            {
                txtValue.Enabled = true;
                chkValue.Enabled = false;
                btnApply.Enabled = true;

                if (tag.Node is JsonValue jv)
                {
                    var underlying = jv.GetValueKind();
                    if (underlying == JsonValueKind.True || underlying == JsonValueKind.False)
                    {
                        txtValue.Visible = false;
                        chkValue.Visible = true;
                        chkValue.Enabled = true;
                        chkValue.Checked = underlying == JsonValueKind.True;
                        return;
                    }
                }

                txtValue.Text = tag.Node.ToJsonString(new JsonSerializerOptions { WriteIndented = false });
            }
            else
            {
                txtValue.Enabled = false;
                chkValue.Enabled = false;
                btnApply.Enabled = false;
                txtValue.Clear();
            }
        }

        // FIXED: Safe pattern matching + type validation + _rootNode
        private void btnApply_Click(object sender, EventArgs e)
        {
            var selected = treeViewJson.SelectedNode;
            var tempTag = selected?.Tag as JsonNodeTag;
            if (tempTag == null || tempTag.Node == null) return;
            var tag = tempTag;

            if (!IsValueNode(tag.Node)) return;

            try
            {
                JsonNode newValueNode;

                if (tag.Node is JsonValue jv && (jv.GetValueKind() == JsonValueKind.True || jv.GetValueKind() == JsonValueKind.False))
                {
                    newValueNode = JsonValue.Create((bool)chkValue.Checked);
                }
                else
                {
                    var raw = txtValue.Text.Trim();
                    if (string.IsNullOrEmpty(raw)) throw new Exception("Value cannot be empty.");

                    var jvTemp = tag.Node as JsonValue ?? throw new Exception("Only primitive values supported.");
                    var valueKind = jvTemp.GetValueKind();

                    if (valueKind == JsonValueKind.Number)
                    {
                        if (!double.TryParse(raw, NumberStyles.Float, CultureInfo.InvariantCulture, out double num))
                            throw new Exception("Invalid number: use decimal format like 123.45 (no letters).");
                        newValueNode = JsonValue.Create(num);
                    }
                    else if (valueKind == JsonValueKind.String)
                    {
                        newValueNode = JsonValue.Create(raw);
                    }
                    else if (valueKind == JsonValueKind.Null)
                    {
                        if (!raw.Equals("null", StringComparison.OrdinalIgnoreCase))
                            throw new Exception("Null values must be exactly 'null'.");
                        newValueNode = JsonValue.Create((object?)null);
                    }
                    else
                    {
                        newValueNode = JsonNode.Parse(raw) ?? JsonValue.Create(raw);
                    }
                }

                // Replace using correct field name
                if (tag.Parent is JsonObject parentObj && tag.PropertyName != null)
                {
                    parentObj[tag.PropertyName] = newValueNode;
                }
                else if (tag.Parent is JsonArray parentArray && tag.Index.HasValue)
                {
                    parentArray[tag.Index.Value] = newValueNode;
                }
                else if (_rootNode == tag.Node)
                {
                    _rootNode = newValueNode;
                }

                tag.Node = newValueNode;
                selected.Tag = tag;
                RefreshNodeDisplay(selected);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Failed to apply value: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // FIXED: Safe refresh with proper Begin/EndUpdate pairing
        private void RefreshNodeDisplay(TreeNode? node)
        {
            if (node == null || node.Tag is not JsonNodeTag refreshTag) return;

            treeViewJson.BeginUpdate();
            node.Nodes.Clear();

            if (IsValueNode(refreshTag.Node))
            {
                treeViewJson.EndUpdate();
                return;
            }

            BuildChildren(node, refreshTag.Node);
            treeViewJson.EndUpdate();
        }

        private sealed class JsonNodeTag
        {
            public JsonNode Node;
            public JsonNode? Parent;  // either JsonObject or JsonArray
            public string? PropertyName;
            public int? Index;

            public JsonNodeTag(JsonNode node, JsonNode? parent, string? propertyName, int? index)
            {
                Node = node;
                Parent = parent;
                PropertyName = propertyName;
                Index = index;
            }
        }
    }
}

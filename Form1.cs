using System;
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

        // When user selects a node in the TreeView
        private void treeViewJson_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node?.Tag is not JsonNodeTag tag || tag.Node == null)
            {
                lblPath.Text = "No node selected.";
                txtValue.Clear();
                return;
            }

            lblPath.Text = GetJsonPath(e.Node);

            if (IsValueNode(tag.Node))
            {
                txtValue.Enabled = true;
                btnApply.Enabled = true;
                txtValue.Text = tag.Node.ToJsonString(new JsonSerializerOptions { WriteIndented = false });
            }
            else
            {
                txtValue.Enabled = false;
                btnApply.Enabled = false;
                txtValue.Clear();
            }
        }

        private static bool IsValueNode(JsonNode node)
        {
            return node is JsonValue;
        }

        private string GetJsonPath(TreeNode node)
        {
            // Build something like: Container.Containers[0].Item.ItemId
            var parts = node
                .FullPath
                .Split(new[] { treeViewJson.PathSeparator }, StringSplitOptions.None);

            return string.Join(".", parts.Skip(1)); // skip (root)
        }

        // Apply button: update the JSON node from txtValue
        private void btnApply_Click(object sender, EventArgs e)
        {
            var selected = treeViewJson.SelectedNode;
            if (selected?.Tag is not JsonNodeTag tag || tag.Node == null)
                return;

            if (!IsValueNode(tag.Node))
                return;

            try
            {
                var raw = txtValue.Text.Trim();

                JsonNode newValueNode;

                bool looksLikeJsonLiteral =
                    raw.Equals("null", StringComparison.OrdinalIgnoreCase) ||
                    raw.Equals("true", StringComparison.OrdinalIgnoreCase) ||
                    raw.Equals("false", StringComparison.OrdinalIgnoreCase) ||
                    raw.StartsWith("\"") ||
                    raw.StartsWith("{") ||
                    raw.StartsWith("[") ||
                    double.TryParse(raw, out _);

                if (!looksLikeJsonLiteral)
                {
                    // Treat as string
                    raw = JsonSerializer.Serialize(raw);
                }

                newValueNode = JsonNode.Parse(raw);

                // Replace in parent
                if (tag.Parent is JsonObject parentObj && tag.PropertyName != null)
                {
                    parentObj[tag.PropertyName] = newValueNode;
                    tag.Node = newValueNode;
                }
                else if (tag.Parent is JsonArray parentArray && tag.Index.HasValue)
                {
                    parentArray[tag.Index.Value] = newValueNode;
                    tag.Node = newValueNode;
                }
                else if (_rootNode == tag.Node)
                {
                    _rootNode = newValueNode;
                }

                selected.Tag = tag;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Failed to apply value:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

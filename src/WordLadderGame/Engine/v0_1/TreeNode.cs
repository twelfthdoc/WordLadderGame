using System.Collections.Generic;

namespace WordLadderGame.Engine.v0_1
{
    public class TreeNode
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public int Generation { get; set; }
        public string Value { get; set; }

        public IList<TreeNode> Children { get; set; }

        public TreeNode()
        {
            Children = new List<TreeNode>();
        }
    }
}
using System.Collections.Generic;

namespace WordLadderGame.Engine.v0_1
{
    public class TreeNode
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public int Generation { get; set; }
        
        public IList<TreeNode> Children { get; set; }

        public TreeNode()
        {
            Children = new List<TreeNode>();
        }
    }
}
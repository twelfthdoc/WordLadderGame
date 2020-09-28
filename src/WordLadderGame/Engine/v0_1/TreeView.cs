namespace WordLadderGame.Engine.v0_1
{
    public class TreeView
    {
        public TreeNode Root { get; set; }
    
        public TreeView()
        {
            Root = new TreeNode
            {
                Id = 1,
                ParentId = 0,
                Generation = 0
            };
        }
    }
}
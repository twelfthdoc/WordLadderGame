namespace WordLadderGame.Engine.v0_1
{
    public class TreeNode
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Value { get; set; }
        
        public TreeNode Parent { get; set; }
    }
}
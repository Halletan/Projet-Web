namespace SpaceAdventures.MVC.Models.Grid
{
    public class GridSettings
    {
        public string Search { get; set; }    // This will allow to search data in the grid
        public int Draw { get; set; }         // This is to know if we need to draw the data-tables
        public int Length { get; set; }       // To know on which column we need to sort
        public string SortColumn { get; set; }   // Number of items to show in the grid
        public string SortOrder { get; set; }    // This field is used to know if it's a ascending or descending
        public int Start { get; set; }          // To know on which number pf page we are.
    }
}

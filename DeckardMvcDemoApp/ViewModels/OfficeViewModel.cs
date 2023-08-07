using DeckardMvcDemoApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DeckardMvcDemoApp.ViewModels
{
    public class OfficeViewModel : Office
    {
        public List<SelectListItem> Buildings { get; set; } = new List<SelectListItem>();
    }
}

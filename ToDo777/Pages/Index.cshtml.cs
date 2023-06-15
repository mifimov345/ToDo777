using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo777.DB;

namespace ToDo777.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string Title { get; set; }

        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
            Tasks = new List<TaskItem>(); // Initialize the Tasks property
        }

        public IList<TaskItem> Tasks { get; set; }

        public string SelectedGroup { get; set; }

        public async Task<IActionResult> OnGetAsync(string selectedGroup)
        {
            Tasks = await _context.Tasks.AsNoTracking().ToListAsync();
            // Check if the selectedGroup parameter is not null or empty
            // and set the SelectedGroup property accordingly
            if (!string.IsNullOrEmpty(selectedGroup))
            {
                SelectedGroup = selectedGroup;
            }

            // Retrieve tasks based on the selected group
            if (SelectedGroup == "Group1")
            {
                Tasks = await _context.Tasks.Where(t => t.Category == "Group1").ToListAsync();
            }
            else if (SelectedGroup == "Group2")
            {
                Tasks = await _context.Tasks.Where(t => t.Category == "Group2").ToListAsync();
            }
            else if (SelectedGroup == "Group3")
            {
                Tasks = await _context.Tasks.Where(t => t.Category == "Group3").ToListAsync();
            }
            else
            {
                Tasks = await _context.Tasks.ToListAsync();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostChangeAsync(int id, string title)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            task.Title = title;
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }


        public async Task<IActionResult> OnPostCreateAsync(string title, string category)
        {
            if (!string.IsNullOrEmpty(title))
            {
                var taskItem = new TaskItem
                {
                    Title = title,
                    Category = category,
                    IsCompleted = false
                };

                _context.Tasks.Add(taskItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var taskItem = await _context.Tasks.FindAsync(id);

            if (taskItem != null)
            {
                _context.Tasks.Remove(taskItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditAsync(int id, string title)
        {
            var taskItem = await _context.Tasks.FindAsync(id);

            if (taskItem != null)
            {
                taskItem.Title = title;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostToggleCompletionAsync(int id)
        {
            var taskItem = await _context.Tasks.FindAsync(id);

            if (taskItem != null)
            {
                taskItem.IsCompleted = !taskItem.IsCompleted;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}

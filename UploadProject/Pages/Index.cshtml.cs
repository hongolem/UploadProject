using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UploadProject.Data;
using UploadProject.Models;

namespace UploadProject.Pages {
    public class IndexModel : PageModel {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<IndexModel> _logger;

        [TempData]
        public string SuccessMessage { get; set; }
        [TempData]
        public string ErrorMessage { get; set; }
        public List<StoredFileListViewModel> Files { get; set; } = new();

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context) {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> OnGet() {
            Files = _context.StoredFiles
                .AsNoTracking()
                .Include(f => f.Author)
                .Include(f => f.Thumbnails)
                .Select(f => new StoredFileListViewModel {
                    Id = f.StoredFileId,
                    ContentType = f.MimeType,
                    OriginalName = f.OriginalName,
                    Uploader = f.Author,
                    UploaderId = f.AuthorId,
                    UploadedAt = f.CreatedAt,
                    ThumbnailCount = f.Thumbnails.Count
                })
                .ToList();
            return Page();
        }

        public class StoredFileListViewModel
        {
            public Guid Id { get; set; }
            public Guid UploaderId { get; set; }
            public User Uploader { get; set; }
            public DateTime UploadedAt { get; set; }
            public string OriginalName { get; set; }
            public string ContentType { get; set; }
            public int ThumbnailCount { get; set; }
        }
        public IActionResult OnGetDownload(string filename) {
            var file = _context.StoredFiles.FirstOrDefault(f => f.OriginalName == filename);
            if (file != null) {
                return File(file.Blob, file.MimeType, file.OriginalName);
            } else {
                ErrorMessage = "There is no such file.";
                return RedirectToPage();
            }
        }

        public async Task<IActionResult> OnGetThumbnail(string filename, ThumbnailType type = ThumbnailType.Square)
        {
            StoredFile file = await _context.StoredFiles
                .AsNoTracking()
                .Where(f => f.StoredFileId == Guid.Parse(filename))
                .SingleOrDefaultAsync();
            if (file == null)
            {
                return NotFound("no record for this file");
            }
            Thumbnail thumbnail = await _context.Thumbnails
                .AsNoTracking()
                .Where(t => t.StoredFileId == Guid.Parse(filename) && t.Type == type)
                .SingleOrDefaultAsync();
            if (thumbnail != null)
            {
                return File(thumbnail.Blob, file.MimeType);
            }
            return NotFound("no thumbnail for this file");
        }
    }
}

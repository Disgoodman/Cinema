using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace Cinema.Areas.Admin.Pages
{
    [Authorize(Roles = "Administrator")]
    public class BackupSqlModel : PageModel
    {
        private readonly ExportSqlOptions _options;

        public BackupSqlModel(IOptions<ExportSqlOptions> options)
        {
            _options = options.Value;
        }

        public FileContentResult OnGet()
        { 
            var process = new Process();

            var folder = _options.PostgresSqlFolder;
            var connString = _options.GetConnectionUri();
            var file = _options.BackupBatName;
            var resName = _options.ResultFile;

            var startInfo = new ProcessStartInfo
            {
                FileName = file,
                Arguments = $"\"{folder}\" {resName} {connString}",
                CreateNoWindow = true,
                UseShellExecute = false
            };

            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
            
            var mas = System.IO.File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory, resName));
            return File(mas, "application/force-download", resName);
        }
    }
}

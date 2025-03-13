using System.Diagnostics;

namespace Waifu.Application.Helper
{

    public class Waifu2xConsole
    {
        //Usage: waifu2x-ncnn-vulkan -i infile -o outfile[options]...
        //-h show this help
        //-v verbose output
        //-i input-path input image path(jpg/png/webp) or directory
        //-o output-path output image path(jpg/png/webp) or directory
        //-n noise-level denoise level(-1/0/1/2/3, default=0)
        //-s scale             upscale ratio(1/2/4/8/16/32, default=2)
        //-t tile-size tile size(>=32/0=auto, default=0) can be 0,0,0 for multi-gpu
        //-m model-path waifu2x model path(default=models-cunet)
        //-g gpu-id gpu device to use(-1=cpu, default=auto) can be 0,1,2 for multi-gpu
        //-j load:proc:save thread count for load/proc/save(default=1:2:2) can be 1:2,2,2:2 for multi-gpu
        //-x enable tta mode
        //-f format            output image format(jpg/png/webp, default=ext/png)

        // waifu2x.exe -i "C:\Users\UnderKo\Downloads\input.jpg" -o "C:\Users\UnderKo\Downloads\output.p

        public static string CreateConsoleCommand(
            string input, string output,
            string noiseLevel = "0",
            string scale = "2",
            string tileSize = "0",
            string gpuId = "0",
            string threadCount = "1:2:2",
            string format = "ext/png",
            string filter = "Lanczos",
            string models = ""
            )
        {
            var v = new Dictionary<string, string>()
            {
                { "-i" , $"\"{input}\"" },
                { "-o",  $"\"{output}\"" },
                { "-n",  $"{noiseLevel}" },
                { "-s",  $"{scale}" },
                { "-g",  $"{gpuId}" },
                { "-t",  $"{tileSize}" },
                { "-filter ",  $"{filter}" },
                { "-model_dir" , $"\"{models}\"" }
            };


            return string.Join(" ", v.Select(x => $"{x.Key} {x.Value}"));
        }


        public static async Task Waifu2xConsoleRun(string keyValuePairs
            )
        {
            using (Process process = new Process())
            {




                process.StartInfo.Arguments = keyValuePairs;

                process.StartInfo.FileName = "waifu2x.exe";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.CreateNoWindow = true;


                process.OutputDataReceived += (sender, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        Debug.WriteLine(e.Data);
                    }
                };


                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                await Task.Run(() => process.WaitForExit());
            }
        }


    }
}

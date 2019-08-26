using Capnp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace CapnpC
{
    class Program
    {
        const string MAGIC_DEBUG_ENVVAR = "DEBUG_CAPNP_BINFILE";

        static string InvokeFrontend(string[] args)
        {
            string capnpbin = Path.GetTempFileName();

            Environment.SetEnvironmentVariable(MAGIC_DEBUG_ENVVAR, capnpbin);

            using (var compiler = new Process())
            {
                var argList = new List<string>();
                argList.Add("compile");
                argList.Add($"-o\"{Assembly.GetExecutingAssembly().Location}\""); // Does not yet work - it's a dll...

                compiler.StartInfo.FileName = "capnp.exe";
                compiler.StartInfo.Arguments = string.Join(' ', argList);
                compiler.StartInfo.UseShellExecute = false;
                compiler.StartInfo.RedirectStandardOutput = true;
                compiler.StartInfo.RedirectStandardError = true;
                compiler.Start();

                Console.WriteLine(compiler.StandardOutput.ReadToEnd());
                Console.WriteLine(compiler.StandardError.ReadToEnd());

                compiler.WaitForExit();

                if (compiler.ExitCode != 0)
                {
                    Environment.Exit(compiler.ExitCode);
                }
            }

            return capnpbin;
        }

        static void Main(string[] args)
        {
            Stream input;

            if (args.Length > 0)
            {
                // This branch is for debugging the generator (because the way it's used by capnp is so painful for attaching a debugger).

                string capnpbin;

                if (args.Length == 1 && Path.GetExtension(args[0]).Equals(".capnpbin", StringComparison.OrdinalIgnoreCase))
                {
                    // Only there for backwards compatibility
                    capnpbin = args[0];
                }
                else
                {
                    capnpbin = InvokeFrontend(args);
                }

                input = new FileStream(capnpbin, FileMode.Open, FileAccess.Read);
            }            
            else
            {
                string tempFile = Environment.GetEnvironmentVariable(MAGIC_DEBUG_ENVVAR);

                if (!string.IsNullOrWhiteSpace(tempFile))
                {
                    // Just output binary file

                    using (input = Console.OpenStandardInput())
                    using (var file = File.Open(tempFile, FileMode.OpenOrCreate))
                    {
                        var buffer = new byte[10000];
                        int total = 0;

                        do
                        {
                            int got = input.Read(buffer, 0, buffer.Length);
                            if (got == 0) break;
                            file.Write(buffer, 0, got);
                            total += got;
                        } while (true);

                        Console.WriteLine($"Received {total} bytes");
                    }

                    Environment.Exit(0);
                }

                Console.WriteLine("Cap'n Proto C# code generator backend");
                Console.WriteLine("expecting binary-encoded code generation request from standard input");

                input = Console.OpenStandardInput();
            }

            try
            {
                WireFrame segments;

                using (input)
                {
                    segments = Framing.ReadSegments(input);
                }

                var dec = DeserializerState.CreateRoot(segments);
                var reader = Schema.CodeGeneratorRequest.Reader.Create(dec);
                var model = Model.SchemaModel.Create(reader);
                var codeGen = new Generator.CodeGenerator(model, new Generator.GeneratorOptions());
                codeGen.Generate();
            }
            catch (Exception exception)
            {
                Console.Error.WriteLine(exception.Message);
                Environment.ExitCode = -1;
            }
        }
    }
}

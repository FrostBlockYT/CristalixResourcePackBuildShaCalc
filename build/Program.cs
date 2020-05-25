using System;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

namespace build
{
    class Program
    {
        static void Main(string[] args)
        {
            if (File.Exists("risepack_old.zip"))
                File.Delete("risepack_old.zip");
            if (File.Exists("risepack.zip"))
                File.Copy("risepack.zip", "risepack_old.zip"); File.Delete("risepack.zip");
            if (File.Exists("sha_old.txt"))
                File.Delete("sha_old.txt");
            if (File.Exists("sha.txt"))
                File.Copy("sha.txt", "sha_old.txt"); File.Delete("sha.txt");

            string current_folder = Directory.GetCurrentDirectory();
            string assets_folder = current_folder + "\\risepack";
            string zipPath = current_folder + "\\risepack.zip";
            try
            {
            ZipFile.CreateFromDirectory(assets_folder,zipPath);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error on creating zip:" + ex);
            }

            Console.WriteLine("PackBuilded!\nGenerating SHA-1\n");
            FileStream fop = File.OpenRead("risepack.zip");
            string chksum = BitConverter.ToString(System.Security.Cryptography.SHA1.Create().ComputeHash(fop));
            Console.WriteLine(chksum);
            File.WriteAllText("sha.txt", chksum);
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();


        }
    }
}

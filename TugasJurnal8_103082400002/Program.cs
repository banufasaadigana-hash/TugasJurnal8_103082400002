using System;

namespace tpmodul8_103082400002
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=========================================");
            Console.WriteLine("TUGAS PENDAHULUAN MODUL 8 - NIM: 103082400002");
            Console.WriteLine("APLIKASI PENGECEKAN SUHU & GEJALA DEMAM");
            Console.WriteLine("=========================================\n");

            // ==================================================
            // LOAD KONFIGURASI DARI FILE
            // ==================================================
            CovidConfig config = CovidConfig.LoadConfig();

            Console.WriteLine("Konfigurasi saat ini:");
            Console.WriteLine($"Satuan Suhu: {config.satuan_suhu}");
            Console.WriteLine($"Batas Hari Demam: {config.batas_hari_deman} hari");
            Console.WriteLine($"Pesan Diterima: {config.pesan_diterima}");
            Console.WriteLine($"Pesan Ditolak: {config.pesan_ditolak}");
            Console.WriteLine();

            // ==================================================
            // INPUT DARI USER
            // ==================================================
            Console.WriteLine("--- FORM PENDAFTARAN MASUK GEDUNG ---");

            // Input 1: Suhu badan
            Console.Write($"Berapa suhu badan anda saat ini? Dalam nilai {config.satuan_suhu}: ");
            double suhu = Convert.ToDouble(Console.ReadLine());

            // Input 2: Hari demam
            Console.Write("Berapa hari yang lalu (perkiraan) anda terakhir memiliki gejala demam? ");
            int hariDemam = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            // ==================================================
            // PENGECEKAN KONDISI
            // ==================================================

            // Cek kondisi: suhu valid DAN hari demam valid
            bool suhuValid = config.IsSuhuValid(suhu);
            bool hariValid = config.IsHariDemanValid(hariDemam);

            if (suhuValid && hariValid)
            {
                // Kondisi terpenuhi
                Console.WriteLine("✅ HASIL: " + config.pesan_diterima);
                Console.WriteLine("✅ Anda diperbolehkan masuk ke dalam gedung.");
            }
            else
            {
                // Kondisi tidak terpenuhi
                Console.WriteLine("❌ HASIL: " + config.pesan_ditolak);

                // Informasi tambahan mengapa ditolak
                if (!suhuValid)
                {
                    Console.WriteLine($"❌ Alasan: Suhu badan {suhu} {config.satuan_suhu} tidak dalam range yang ditentukan.");
                    if (config.satuan_suhu.ToLower() == "celcius")
                        Console.WriteLine("   (Range yang diperbolehkan: 36.5 - 37.5 derajat celcius)");
                    else
                        Console.WriteLine("   (Range yang diperbolehkan: 97.7 - 99.5 derajat fahrenheit)");
                }
                if (!hariValid)
                {
                    Console.WriteLine($"❌ Alasan: Terakhir demam {hariDemam} hari yang lalu, melebihi batas {config.batas_hari_deman} hari.");
                }
                Console.WriteLine("❌ Anda TIDAK diperbolehkan masuk ke dalam gedung.");
            }

            Console.WriteLine();

            // ==================================================
            // MENANYAKAN APAKAH INGIN MENGGANTI SATUAN
            // ==================================================
            Console.Write("Apakah Anda ingin mengubah satuan suhu? (y/n): ");
            string jawaban = Console.ReadLine()?.ToLower();

            if (jawaban == "y" || jawaban == "yes")
            {
                Console.WriteLine("\n--- MENGUBAH SATUAN SUHU ---");
                config.UbahSatuan();  // Panggil method UbahSatuan
                Console.WriteLine($"Satuan suhu sekarang: {config.satuan_suhu}");
                Console.WriteLine("Perubahan telah disimpan ke file covid_config.json");

                // Tampilkan konfigurasi terbaru
                Console.WriteLine("\nKonfigurasi terbaru yang tersimpan:");
                Console.WriteLine($"Satuan Suhu: {config.satuan_suhu}");
                Console.WriteLine($"Batas Hari Demam: {config.batas_hari_deman}");
                Console.WriteLine($"Pesan Diterima: {config.pesan_diterima}");
                Console.WriteLine($"Pesan Ditolak: {config.pesan_ditolak}");
            }
            else
            {
                Console.WriteLine("Satuan suhu tidak diubah.");
            }

            Console.WriteLine("\n=========================================");
            Console.WriteLine("Program selesai! Tekan Enter untuk keluar...");
            Console.ReadLine();
        }
    }
}
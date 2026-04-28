using Newtonsoft.Json;
using System;
using System.IO;

namespace tpmodul8_103082400002
{
    public class CovidConfig
    {
        // Properties sesuai format JSON
        public string satuan_suhu { get; set; }
        public int batas_hari_deman { get; set; }
        public string pesan_ditolak { get; set; }
        public string pesan_diterima { get; set; }

        // Nama file konfigurasi
        private const string fileName = "covid_config.json";

        // Constructor default (menggunakan nilai default)
        public CovidConfig()
        {
            satuan_suhu = "celcius";
            batas_hari_deman = 14;
            pesan_ditolak = "Anda tidak diperbolehkan masuk ke dalam gedung ini";
            pesan_diterima = "Anda dipersilahkan untuk masuk ke dalam gedung ini";
        }

        // Method untuk membaca konfigurasi dari file JSON
        public static CovidConfig LoadConfig()
        {
            if (File.Exists(fileName))
            {
                // Jika file ada, baca dan deserialize
                string json = File.ReadAllText(fileName);
                return JsonConvert.DeserializeObject<CovidConfig>(json);
            }
            else
            {
                // Jika file tidak ada, buat konfigurasi default
                CovidConfig defaultConfig = new CovidConfig();
                defaultConfig.SaveConfig(); // Simpan ke file
                return defaultConfig;
            }
        }

        // Method untuk menyimpan konfigurasi ke file JSON
        public void SaveConfig()
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(fileName, json);
        }

        // Method untuk mengubah satuan suhu (celcius <-> fahrenheit)
        public void UbahSatuan()
        {
            if (satuan_suhu.ToLower() == "celcius")
            {
                satuan_suhu = "fahrenheit";
                Console.WriteLine("Satuan suhu telah diubah menjadi FAHRENHEIT");
            }
            else if (satuan_suhu.ToLower() == "fahrenheit")
            {
                satuan_suhu = "celcius";
                Console.WriteLine("Satuan suhu telah diubah menjadi CELCIUS");
            }
            else
            {
                Console.WriteLine("Satuan suhu tidak dikenal, diubah ke celcius");
                satuan_suhu = "celcius";
            }

            // Simpan perubahan ke file
            SaveConfig();
        }

        // Method untuk validasi suhu berdasarkan satuan
        public bool IsSuhuValid(double suhu)
        {
            if (satuan_suhu.ToLower() == "celcius")
            {
                // Range celcius: 36.5 - 37.5
                return suhu >= 36.5 && suhu <= 37.5;
            }
            else if (satuan_suhu.ToLower() == "fahrenheit")
            {
                // Range fahrenheit: 97.7 - 99.5
                return suhu >= 97.7 && suhu <= 99.5;
            }
            return false;
        }

        // Method untuk validasi hari demam
        public bool IsHariDemanValid(int hari)
        {
            // Hari demam harus kurang dari batas_hari_deman
            return hari < batas_hari_deman;
        }
    }
}
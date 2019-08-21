using Rocket.API;

namespace DaePvPLimitleyici
{
    public class PvPLimitleyiciYapılandırma : IRocketPluginConfiguration
    {
        public bool BelirliKişilerHasarVerebilsin { get; set; }
        public string HasarVermeYetkisi { get; set; }

        public bool YetkiliYokkenLimitle { get; set; }
        public string Yetkili { get; set; }

        public bool OyuncuSayısınaGöreLimitle { get; set; }
        public int MinimumOyuncuSayısı { get; set; }

        public void LoadDefaults()
        {
            BelirliKişilerHasarVerebilsin = true;
            HasarVermeYetkisi = "HasarVerebilir";

            YetkiliYokkenLimitle = false;
            Yetkili = "Yetkili";

            OyuncuSayısınaGöreLimitle = true;
            MinimumOyuncuSayısı = 4;
        }
    }
}
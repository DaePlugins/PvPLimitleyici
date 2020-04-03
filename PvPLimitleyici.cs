using System.Linq;
using Rocket.API;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Player;
using SDG.Unturned;

namespace DaePvPLimitleyici
{
    public class PvPLimitleyici : RocketPlugin<PvPLimitleyiciYapılandırma>
    {
        private bool _yetkiliYok;

        protected override void Load()
        {
            if (Configuration.Instance.YetkiliYokkenLimitle)
            {
                _yetkiliYok = !Provider.clients.Any(s => UnturnedPlayer.FromSteamPlayer(s).HasPermission($"dae.pvplimitleyici.{Configuration.Instance.Yetkili}"));

                U.Events.OnPlayerConnected += OyuncuBağlandığında;
                U.Events.OnPlayerDisconnected += OyuncuAyrıldığında;
            }

            DamageTool.damagePlayerRequested += HasarAlındığında;
        }

        protected override void Unload()
        {
            if (Configuration.Instance.YetkiliYokkenLimitle)
            {
                U.Events.OnPlayerConnected -= OyuncuBağlandığında;
                U.Events.OnPlayerDisconnected -= OyuncuAyrıldığında;
            }
            
            DamageTool.damagePlayerRequested -= HasarAlındığında;
        }

        private void OyuncuBağlandığında(UnturnedPlayer oyuncu)
        {
            if (_yetkiliYok && oyuncu.HasPermission($"dae.pvplimitleyici.{Configuration.Instance.Yetkili}"))
            {
                _yetkiliYok = false;
            }
        }

        private void OyuncuAyrıldığında(UnturnedPlayer oyuncu)
        {
            if (!_yetkiliYok)
            {
                _yetkiliYok = !Provider.clients.Any(s => UnturnedPlayer.FromSteamPlayer(s).HasPermission($"dae.pvplimitleyici.{Configuration.Instance.Yetkili}")
                                                         && s.playerID.steamID.m_SteamID != oyuncu.CSteamID.m_SteamID);
            }
        }

        private void HasarAlındığında(ref DamagePlayerParameters parametreler, ref bool hasarVerebilir)
        {
            if (Configuration.Instance.BelirliKişilerHasarVerebilsin && !UnturnedPlayer.FromCSteamID(parametreler.killer).HasPermission($"dae.pvplimitleyici.{Configuration.Instance.HasarVermeYetkisi}")
                || Configuration.Instance.YetkiliYokkenLimitle && _yetkiliYok
                || Configuration.Instance.OyuncuSayısınaGöreLimitle && Configuration.Instance.MinimumOyuncuSayısı > Provider.clients.Count)
            {
                hasarVerebilir = false;
            }
        }
    }
}
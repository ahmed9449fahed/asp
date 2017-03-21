using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using DartProjekt.Controllers;

namespace DartProjekt.Models
{
    public class DbManager
    {
        public static void InsertPlayer(string vorName,string nachName)
        {

            DB_A14A47_DBDartEntities5 context =new DB_A14A47_DBDartEntities5();
            TbSpieleren spieler=new TbSpieleren();
            spieler.VorName = vorName;
            spieler.NachName = nachName;
            context.TbSpielerens.Add(spieler);
            context.SaveChanges();
        }

      
        public static void InsertGroup(List<PLayersInfos> players,string groupename,string gametype )
        {
            int GroupeId=new int();
            DB_A14A47_DBDartEntities5 context = new DB_A14A47_DBDartEntities5();
            TbGruppen groupe=new TbGruppen();
            groupe.GroupeName = groupename;
            context.TbGruppens.Add(groupe);
            context.SaveChanges();
           
            foreach (TbGruppen gruppen in context.TbGruppens)
            {
                if (gruppen.GroupeName == groupename) GroupeId = gruppen.GroupeID;
            }
            Tb_SpielerGruppe spielerGruppeninfo = new Tb_SpielerGruppe();
            foreach (PLayersInfos info in players)
            {
                spielerGruppeninfo.GruupeId =GroupeId;
                spielerGruppeninfo.SpielerId = info.Id;
                spielerGruppeninfo.Spieletype = gametype;
                context.Tb_SpielerGruppe.Add(spielerGruppeninfo);
                context.SaveChanges();
            }
        }
        public static void Insertwinner(PLayersInfos info,int groupeId)
        {
            DB_A14A47_DBDartEntities5 context = new DB_A14A47_DBDartEntities5();
            Tb_winner winner=new Tb_winner();
            winner.PLayerId = info.Id;
            winner.GroupeId = groupeId;
            context.Tb_winner.Add(winner);
            context.SaveChanges();
        }
        public static List<PLayersInfos> GetplayersName()
        {
            List<PLayersInfos> spieleren=new List<PLayersInfos>();
            DB_A14A47_DBDartEntities5 context = new DB_A14A47_DBDartEntities5();
            foreach (TbSpieleren spieler in context.TbSpielerens)
            {
                PLayersInfos info =new PLayersInfos();
                info.VorName = spieler.VorName;
                info.NachName = spieler.NachName;
                info.Id = spieler.Id;
                spieleren.Add(info);
            }
            return spieleren;
        }

        public static List<WinnerInfo> GetWinnwrInfos()
        {
            List<WinnerInfo> winnerInfos = new List<WinnerInfo>();
            DB_A14A47_DBDartEntities5 context = new DB_A14A47_DBDartEntities5();
            foreach (Tb_winner winner in context.Tb_winner)
            {
                WinnerInfo info = new WinnerInfo()
                {
                    WinnerName = GetwinnerNamebyId(winner.PLayerId),
                    GroupeName = GetGroupeNamebyId(winner.GroupeId)

                };
                
             winnerInfos.Add(info);
            }
            return winnerInfos;
        }
        public static string GetwinnerNamebyId(int id)
        {
            DB_A14A47_DBDartEntities5 context= new DB_A14A47_DBDartEntities5();
            foreach (TbSpieleren spieler in context.TbSpielerens)
            {
                if (spieler.Id == id)
                {
                    var winnerName = spieler.VorName + " " + spieler.NachName;
                    return winnerName;
                }
            }
            return null;
        }

        public static List<Groupsinfo> GetAllGroupsinfos()
        {
            List<Groupsinfo> groupsinfos=new List<Groupsinfo>();
            DB_A14A47_DBDartEntities5 context=new DB_A14A47_DBDartEntities5();
            foreach (TbGruppen group in context.TbGruppens)
            {
                Groupsinfo _group=new Groupsinfo();
                _group.GroupName = group.GroupeName;
                _group.GroupMembers = GetAllplayersNamebygroupId(group.GroupeID);
                _group.GroupId = group.GroupeID;
                _group.GameType = GetGameTypepyGroupId(group.GroupeID);
                groupsinfos.Add(_group);
                
            }
           
            return groupsinfos;
        }
        public static List<int> GetCountofGroups()
        {
            List<int> groupidlist=new List<int>();
            DB_A14A47_DBDartEntities5 context = new DB_A14A47_DBDartEntities5();
            foreach (TbGruppen group in context.TbGruppens)
            {
                groupidlist.Add(group.GroupeID);
            }
            return groupidlist;
        }

        public static string GetGameTypepyGroupId(int id)
        {
            DB_A14A47_DBDartEntities5 context=new DB_A14A47_DBDartEntities5();
            foreach (Tb_SpielerGruppe group in context.Tb_SpielerGruppe)
            {
                if (group.GruupeId == id)
                    return group.Spieletype;
            }
            return null;
        }

        public static List<string> GetAllplayersNamebygroupId(int id)
        {
            List<string> playersName=new List<string>();
            DB_A14A47_DBDartEntities5 context=new DB_A14A47_DBDartEntities5();
            foreach (Tb_SpielerGruppe spieler in context.Tb_SpielerGruppe)
            {
                if(spieler.GruupeId==id)
                    playersName.Add(GetplayersNamebyId(spieler.SpielerId));

            }
            return playersName;
        }
        public static List<PLayersInfos> GetAllplayersInfobygroupId(int id)
        {
            List<PLayersInfos> playersName = new List<PLayersInfos>();
            DB_A14A47_DBDartEntities5 context = new DB_A14A47_DBDartEntities5();
            foreach (Tb_SpielerGruppe spieler in context.Tb_SpielerGruppe)
            {
                if (spieler.GruupeId == id)
                    playersName=GetAllplayersinfobygroupId(id);

            }
            return playersName;
        }
        public static string GetplayersNamebyId(int id)
        {
            DB_A14A47_DBDartEntities5 context=new DB_A14A47_DBDartEntities5();
            foreach (TbSpieleren spieler in context.TbSpielerens)
            {
                if (spieler.Id == id)
                    return spieler.VorName +" "+ spieler.NachName;
            }
            return null;
        }
        public static PLayersInfos GetplayersinfobyId(int id)
        {
            DB_A14A47_DBDartEntities5 context = new DB_A14A47_DBDartEntities5();
            foreach (TbSpieleren spieler in context.TbSpielerens)
            {
                if (spieler.Id == id)
                {
                    PLayersInfos info = new PLayersInfos()
                    {
                        Id = id,
                        VorName = spieler.VorName,
                        NachName = spieler.NachName,

                    };
                    return info;

                }
                   
            }
            return null;
        }
        public static List<PLayersInfos> GetAllplayersinfobygroupId(int id)
        {
            List<PLayersInfos> playersName = new List<PLayersInfos>();
            DB_A14A47_DBDartEntities5 context = new DB_A14A47_DBDartEntities5();
            foreach (Tb_SpielerGruppe spieler in context.Tb_SpielerGruppe)
            {
                if (spieler.GruupeId == id)
                    playersName.Add(GetplayersinfobyId(spieler.SpielerId));

            }
            return playersName;
        }
        public static string GetGroupeNamebyId(int id)
        {
            DB_A14A47_DBDartEntities5 context = new DB_A14A47_DBDartEntities5();
            foreach (TbGruppen groupe in context.TbGruppens)
            {
                if (groupe.GroupeID == id)
                {
                    var groupeName = groupe.GroupeName;
                    return groupeName;
                }
            }
            return null;
        }

        public static void ResetAll()
        {
            DB_A14A47_DBDartEntities5 context=new DB_A14A47_DBDartEntities5();
            foreach (TbSpieleren spieler in context.TbSpielerens)
            {
                context.TbSpielerens.Remove(spieler);
            }
            foreach (TbGruppen group in context.TbGruppens)
            {
                context.TbGruppens.Remove(group);
            }
            foreach (Tb_SpielerGruppe spieler in context.Tb_SpielerGruppe)
            {
                context.Tb_SpielerGruppe.Remove(spieler);
            }
            foreach (Tb_winner spieler in context.Tb_winner)
            {
                context.Tb_winner.Remove(spieler);
            }
            context.SaveChanges();
        }

    }


    public class PLayersInfos
    {
        public string VorName { get; set; }
        public string NachName { get; set; }
        public int Id { get; set; }

    }
    public class GruppenInfo
    {
        public GruppenInfo(int gruppeId)
        {
            GruppeId = gruppeId;
        }

        public string GruppeName { get; set; }
        public int GruppeId { get;}

    }
    

}
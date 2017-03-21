using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using DartProjekt.Models;
using Microsoft.ApplicationInsights.WindowsServer;

namespace DartProjekt.Controllers
{
    public class HomeController : Controller
    {
        
        public static List<PLayersInfos> Playernames=new List<PLayersInfos>();
        private static string _gametype;
        private static string _groupname;
       
        public static List<PLayersInfos> selectedplayersinfo = new List<PLayersInfos>();


        public ActionResult Home()
        {
            ViewBag.Title = "Home Page";
            return View();
        }
        public ActionResult Addplayer()
        {
            ViewBag.Title = "Add player Page";
            return View();
        }
        public ActionResult AddGroupe()
        {
            ViewBag.Title = "New Game Page";
            Playernames = DbManager.GetplayersName();
            ViewBag.spielerenNamen = Playernames;
            
            return View("Players");
        }
        public ActionResult AddPlayerToDatenBank()
        {
            var vorName = Request?.Params?.GetValues("playerFirstName").FirstOrDefault();
            var nachName = Request?.Params?.GetValues("playerSecondName").FirstOrDefault();
            if(vorName!=""&&nachName!="")
            DbManager.InsertPlayer(vorName,nachName);
            return View("AddPlayer");
        }
        public ActionResult GameType()
        {
            List<int> selectedplayerId = FindselectedplayersId();
            selectedplayersinfo = FindSelectedPlayersName(selectedplayerId);
            return View();
        }  
        public ActionResult CreatNewGruppe()
        {
            if (Request != null)
            {
                _gametype = Request?.Params?.GetValues("options").FirstOrDefault();
               _groupname = Request?.Params?.GetValues("groubname").FirstOrDefault();
            }             
            DbManager.InsertGroup(selectedplayersinfo,_groupname,_gametype);
            ViewBag.AllGroups = DbManager.GetAllGroupsinfos();
            return View("ShowGroups");
        }

        public ActionResult ShowGroups()
        {
            ViewBag.AllGroups= DbManager.GetAllGroupsinfos();
            return View();
        }

        public ActionResult NewGame()
        {
            _results.Clear();
            ViewBag.AllGroups= DbManager.GetAllGroupsinfos();
            return View();
        }
        public ActionResult Results()
        {
           
            return View();
        }

        public static int Selectedgroupid;
        public static int id;

        private static List<Resultsinfo> _results=new List<Resultsinfo>();
        public ActionResult StartGame()

        {
            
                if (Request.Params.AllKeys.Contains("radio")||Selectedgroupid==id)
                {
                if(Request.Params.AllKeys.Contains("radio"))
                    id =Convert.ToInt32(Request?.Params?.GetValues("radio").FirstOrDefault());
                    ViewBag.playersinfo = DbManager.GetAllplayersinfobygroupId(id);
                    string gametype = DbManager.GetGameTypepyGroupId(id);
                    Selectedgroupid = id;
                    foreach (PLayersInfos info in ViewBag.playersinfo)
                    {
                        Resultsinfo _resultinfo=new Resultsinfo();
                        _resultinfo.PlayerId = info.Id;
                        _resultinfo.Results=new List<int>();
                        _resultinfo.Rest =Convert.ToInt32(gametype);  
                        if (Request.Params.AllKeys.Contains("txtvalue+" + info.Id))
                        {
                            string r = Request?.Params?.GetValues("txtvalue+" + info.Id).FirstOrDefault();
                            if (r != "")
                            {
                                _results.Find(x => x.PlayerId == info.Id)
                                    .Results.Add(
                                        Convert.ToInt32(
                                            Request?.Params?.GetValues("txtvalue+" + info.Id).FirstOrDefault()));
                                _results.Find(x => x.PlayerId == info.Id).Rest -=
                                    Convert.ToInt32(Request?.Params?.GetValues("txtvalue+" + info.Id).FirstOrDefault());
                                if (_results.Find(x => x.PlayerId == info.Id).Rest == 0)
                                {
                                    PLayersInfos winnerinfo = DbManager.GetplayersinfobyId(info.Id);
                                    ViewBag.winnerName = winnerinfo.VorName+" "+winnerinfo.NachName;
                                    
                                    DbManager.Insertwinner(winnerinfo,Selectedgroupid);
                                    return View("TheWinner");
                                }


                            }
                        }
                    if (!_results.Contains(_results.Find(x => x.PlayerId == _resultinfo.PlayerId)))
                        _results.Add(_resultinfo);
                    ViewBag.resultsinfo = _results;
                        ViewBag.maxresultscount = _results[0].Results.Count;
                    }
                }
            
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
        public ActionResult ResetAll()
        {
            DbManager.ResetAll();
            return View("Home");
        }
        public ActionResult Slideshow()
        {
            return View();
        }
        public void Resetvalue()
        {
            selectedplayersinfo.Clear();
            _gametype = "";
            _groupname = "";
            Playernames.Clear();
            
        }
        public List<int> FindselectedplayersId()
        {
            return (from info in Playernames let contains = Request?.Params?.AllKeys.Contains("check+" + info.Id) where contains != null && (bool) contains select info.Id).ToList();
        }
        public ActionResult MatchResults()
        {
            ViewBag.winnerinfos = DbManager.GetWinnwrInfos();
            return View();
        }
        public List<PLayersInfos> FindSelectedPlayersName(List<int> playersId)
        {
            return (from id in playersId from info in Playernames where info.Id == id select info).ToList();
        }
    }

    public class Resultsinfo
    {
        public int PlayerId { get; set; }
        public List<int> Results=new List<int>();
        public int Rest { get; set; }
    }
    public class WinnerInfo
    {
        public string WinnerName { get; set; }
        public string GroupeName { get; set; }
    }

    public class Groupsinfo
    {
       public string GroupName { get; set; }
       public List<string> GroupMembers { get; set; } 
       public int GroupId { get; set; }
       public string GameType { get; set; }
    }
}

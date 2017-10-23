using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;


namespace IdeoTest.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetFileStructure()
        {
            List<Node> list = new List<Node>();
            using (ModelContext dc = new ModelContext())
            {
                list = dc.Nodes.OrderBy(a => a.Name).ToList();
            }

            List<Node> treelist = new List<Node>();
            if (list.Count > 0)
            {
                treelist = BuildTree(list);
            }
            return new JsonResult { Data = new { treeList = treelist }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public void GetTreeview(List<Node> list, Node current, ref List<Node> returnList)
        {
            using (ModelContext db = new ModelContext())
            {
                //get child of current item
                List<Node> childs = list.Where(a => a.ParentId == current.NodeId).ToList();

                current.Nodes = new List<Node>();
                current.Nodes.AddRange(childs);

                foreach (var i in childs)
                {
                    GetTreeview(list, i, ref returnList);
                }
            }
        }
        public List<Node> BuildTree(List<Node> list)
        {
            List<Node> returnList = new List<Node>();
            //find top levels items
            //    var topLevels = list.Where(a => a.ParentId == list.OrderBy(b => b.ParentId).FirstOrDefault().ParentId);
            var topLevels = list.Where(a => a.ParentId == list.OrderBy(b => b.ParentId).FirstOrDefault().ParentId).ToList();
            returnList.AddRange(topLevels);
            foreach (var i in topLevels)
            {
                GetTreeview(list, i, ref returnList);
            }
            return returnList;
        }

    }
}
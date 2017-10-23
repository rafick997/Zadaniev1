using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Collections;

namespace IdeoTest.Controllers
{
    public class NodesController : Controller
    {
        public Node ND = new Node();
        private ModelContext db = new ModelContext();

        // GET: Nodes
        public ActionResult Index(string sortBy)
        {
            ViewBag.SortNameParameter = string.IsNullOrEmpty(sortBy) ? "Name desc" : "";
            ViewBag.SortParentIdParameter = sortBy == "ParentId" ? "ParentId desc desc" : "ParentId";
            ViewBag.SortKindParameter = sortBy == "Kind" ? "Kind desc" : "Kind";


            List<Node> nodes = new List<Node>();
            nodes = db.Nodes.ToList();
            foreach (var node in nodes)
            {
                if (node.NodeId == 19)
                {
                    node.Kind = "Root";
                }
                else if (IsParent(node.NodeId))
                {
                    node.Kind = "Parent";
                }
                else
                {
                    node.Kind = "Child";
                }
            }
            db.SaveChanges();
            switch (sortBy)
            {

                case "Kind":
                    nodes = nodes.OrderBy(a => a.Kind).ToList();
                    break;
                case "Kind desc":
                    nodes = nodes.OrderByDescending(a => a.Kind).ToList();
                    break;
                case "ParentId desc":
                    nodes = nodes.OrderByDescending(a => a.ParentId).ToList();
                    break;
                case "ParentId":
                    nodes = nodes.OrderBy(a => a.ParentId).ToList();
                    break;
                case "Name desc":
                    nodes = nodes.OrderByDescending(x => x.Name).ToList();
                    break;
                default:
                    nodes = nodes.OrderBy(x => x.Name).ToList();
                    break;

            }

            return View(nodes);
        }

        // GET: Nodes/Create
        public ActionResult Create()
        {
          
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NodeId,Name,ParentId")] Node node)
        {
            ModelContext db = new ModelContext();
            List<Node> nodes = db.Nodes.ToList();
            List<Node> nod = nodes.Where(n => n.Name.Equals(node.Name)).ToList();
            if (nod.Count() == 0)
            {
                if (ModelState.IsValid)
                {
                    node.Kind = "Child";
                    db.Nodes.Add(node);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(node);
        }

        // GET: Nodes/Edit/5
        public ActionResult Edit(int? id)
        {
            Node node = db.Nodes.Find(id);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (node == null)
            {
                return HttpNotFound();
            }
            Node root = db.Nodes.Single(i => i.NodeId == 19);
            List<Node> nodesofParentId = GetAllNodes(node);
            List<Node> allNodes = new List<Node>();
            allNodes = db.Nodes.ToList();
            foreach(var a in nodesofParentId)
            {
                allNodes.Remove(a);
            }
            ViewBag.allNodes = allNodes.ToList();
          
            return View(node);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NodeId,Name,ParentId")] Node node)
        {
          
                if (ModelState.IsValid)
                {

                    db.Entry(node).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                 return  RedirectToAction("Index");
        }

        // GET: Nodes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Node node = db.Nodes.Find(id);
            if (node == null)
            {
                return HttpNotFound();
            }
            return View(node);
        }

        public static bool IsNullOrEmpty(IEnumerable source)
        {
            if (source != null)
            {
                foreach (object obj in source)
                {
                    return false;
                }
            }
            return true;
        }
        public bool IsParent(int id)
        {

            ModelContext db = new ModelContext();
            Node node = db.Nodes.Find(id);
            if (getNodesOfParent(node.NodeId).Count() != 0)
            {
                return true;
            }
            else return false;

        }

        public static List<Node> getNodesOfParent(int id)
        {
            using (ModelContext db = new ModelContext())
            {
                List<Node> nodes = db.Nodes.Where(p => p.ParentId == id).ToList();
                return nodes;
            }
        }

        public List<Node> GetAllNodes(Node n)
        {
            List<Node> result = new List<Node>();
            result.Add(n);
            List<Node> list = new List<Node>();
            list = db.Nodes.Where(i => i.ParentId == n.NodeId).ToList();
            foreach (Node child in list)
            {
                result.AddRange(GetAllNodes(child));
            }
            return result;
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRecurently(int id)
        {
            using (ModelContext db = new ModelContext())
            {
                Node node = db.Nodes.Find(id);

                if (IsParent(id))
                {
                    foreach (var Item in getNodesOfParent(id))
                    {
                        DeleteRecurently(Item.NodeId);
                    }
                }
                db.Nodes.Remove(node);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

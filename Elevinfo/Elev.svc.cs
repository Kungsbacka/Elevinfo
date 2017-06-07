using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Elevinfo
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Elev : IElev
    {
        public ElevInfo GetElevInfo(long id)
        {
            ElevInfo result = null;

            ProcapitaBoU p = new ProcapitaBoU();

            DataTable data = p.getElevData(id);
            if(data.Rows.Count > 0)
            {
                result = new ElevInfo();
                result.fornamn = data.Rows[0]["fornamn"].ToString();
                result.efternamn = data.Rows[0]["efternamn"].ToString();
                result.avdelning = data.Rows[0]["avdelning"].ToString();
                result.avdelningId = data.Rows[0]["avdelningid"].ToString();
                result.enhet = data.Rows[0]["enhet"].ToString();
                result.enhetId = data.Rows[0]["enhetid"].ToString();
                result.skolform = data.Rows[0]["skolform"].ToString();
                result.skolformId = data.Rows[0]["skolformid"].ToString();
                result.klass = data.Rows[0]["klass"].ToString();
                result.klassId = data.Rows[0]["klassId"].ToString();
                result.klassarskurs = data.Rows[0]["klassarskurs"].ToString();
                result.avdelning = data.Rows[0]["avdelning"].ToString();
            } else
            {
                return null;
            }

            data = p.getElevGruppInfo(id);
            if(data.Rows.Count > 0)
            {
                List<Grupp> gs = new List<Grupp>();
                foreach(DataRow r in data.Rows)
                {
                    Grupp g = new Grupp
                    {
                        namn = r["grupp"].ToString(),
                        id = r["gruppid"].ToString(),
                        period = r["grupperiod"].ToString()
                    };
                    gs.Add(g);
                }
                result.grupper = gs.ToArray();
            }
            return result;
        }

        public Mentor[] GetMentors(long id)
        {
            List<Mentor> result = new List<Mentor>();
            ProcapitaBoU p = new ProcapitaBoU();
            DataTable ms = p.getElevMentor(id);
            foreach(DataRow row in ms.Rows)
            {
                result.Add(new Mentor { efternamn = row["efternamn"].ToString(), fornamn = row["fornamn"].ToString(), mail = row["mail"].ToString() });
            }

            return result.ToArray();
        }

        public void GetOptions()
        {
        }
    }
}

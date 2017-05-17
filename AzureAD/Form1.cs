using Microsoft.Azure.ActiveDirectory.GraphClient;
using Microsoft.Azure.ActiveDirectory.GraphClient.Extensions;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AzureAD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            GraphServiceClient gsc = await AuthenticationHelper.GetGraphServiceClientAsUser();
            IGraphServiceUsersCollectionPage iucp = await 
                gsc.Users.Request().Filter("").GetAsync();

            bool encore = true;

            while(encore)
            {
                TB1.AppendText("*****************" + Environment.NewLine);
                List<User> ul = iucp.CurrentPage.ToList();
                foreach (User u in ul)
                {
                    TB1.AppendText(u.DisplayName + Environment.NewLine);
                }

                IGraphServiceUsersCollectionRequest iucr = iucp.NextPageRequest;

                if(iucr == null)
                {
                    encore = false;
                }
                else
                {
                    iucp = await iucr.GetAsync();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AuthenticationHelper.DoReset = true;
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            GraphServiceClient gsc = await AuthenticationHelper.GetGraphServiceClientAsUser();
            IGraphServiceGroupsCollectionPage gcp = await gsc.Groups.Request().GetAsync();
            foreach(Group g in gcp.CurrentPage)
            {
                TB1.AppendText(g.DisplayName + Environment.NewLine);
                string type, etab;
                switch (g.DisplayName)
                {
                    case "/vinci":
                        type = "etab";
                        etab = "0754475g";
                        break;
                    case "/vinci/eleves":
                        type = "eleves";
                        etab = "0754475g";
                        break;
                    case "/vinci/enseignants":
                        type = "enseignant";
                        etab = "0754475g";
                        break;
                    case "/vinci/eleves/T S":
                        type = "classe";
                        etab = "0754475g";
                        break;
                    default:
                        type = null;
                        etab = null;
                        break;
                }
                Group gg = new Group();
                Dictionary<string, object> ext = new Dictionary<string, object>();
                ext["extension_d5ac20ff738d4a5da8fe0e8b3887be8a_etabID"] = etab;
                gg.AdditionalData = ext;
                string gid = g.AdditionalData["objectId"].ToString();
                //await gsc.Groups[gid].Request().UpdateAsync(gg);
                TB1.AppendText("UPDATED**************" + Environment.NewLine);


            }
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            GraphServiceClient gsc = await AuthenticationHelper.GetGraphServiceClientAsUser();
            IGraphServiceGroupsCollectionPage gcp = await gsc.Groups.Request().GetAsync();
            foreach (Group g in gcp.CurrentPage)
            {
                TB1.AppendText(g.DisplayName + Environment.NewLine);
                TB1.AppendText("  nickname : " + g.MailNickname + Environment.NewLine);
                object val;
                if (g.AdditionalData.TryGetValue("objectId", out val) && val != null)
                {
                    TB1.AppendText("  Id : " + val.ToString() + Environment.NewLine);
                }
                if (g.AdditionalData.TryGetValue("extension_d5ac20ff738d4a5da8fe0e8b3887be8a_etabID", out val) && val != null)
                {
                    TB1.AppendText("  Etab : " + val.ToString() + Environment.NewLine);
                }
                if (g.AdditionalData.TryGetValue("extension_d5ac20ff738d4a5da8fe0e8b3887be8a_Niveau", out val) && val != null)
                {
                    TB1.AppendText("  Type de groupe : " + val.ToString() + Environment.NewLine);
                }
                string gid = g.AdditionalData["objectId"].ToString();
                IGroupMembersCollectionWithReferencesPage gmp = await gsc.Groups[gid].Members.Request().GetAsync();
                TB1.AppendText("  Membres : " + Environment.NewLine);
                foreach (DirectoryObject o in gmp.CurrentPage)
                {
                    object oo;
                    if(o.AdditionalData.TryGetValue("objectType", out oo) && (oo != null))
                    {
                        switch (oo.ToString())
                        {
                            case "User":
                                User u = new User();
                                u.AdditionalData = o.AdditionalData;
                                TB1.AppendText("    User : " + u.DisplayName + Environment.NewLine);
                                break;
                            case "Group":
                                break;
                                Group mg = o as Group;
                                TB1.AppendText("    Group : " + mg.DisplayName + Environment.NewLine);
                                break;
                            default:
                                break;
                        }
                    }

                }
            }
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            GraphServiceClient gsc = await AuthenticationHelper.GetGraphServiceClientAsUser();
            Group g = new Group
            {
                DisplayName = "/vinci/classe/2GT1",
                MailEnabled = false,
                SecurityEnabled = true,
                MailNickname = "/vinci/classe/2GT1"
            };
            Dictionary<string, object> ext = new Dictionary<string, object>();
            ext["extension_d5ac20ff738d4a5da8fe0e8b3887be8a_Niveau"] = "classe";
            ext["extension_d5ac20ff738d4a5da8fe0e8b3887be8a_etabID"] = "0754475g";
            g.AdditionalData = ext;
            Group gg =  await gsc.Groups.Request().AddAsync(g);
            TB1.AppendText("Groupe " + gg.DisplayName + " créé");

        }

        private void button6_Click(object sender, EventArgs e)
        {
            ActiveDirectoryClient adc = await AuthenticationHelper.GetActiveDirectoryClientAsUser();
            ISubscribedSkuCollection iskc = adc.SubscribedSkus;
            IPagedCollection<ISubscribedSku> skl = await iskc.ExecuteAsync();
            foreach (ISubscribedSku sku in skl.CurrentPage.ToList())
            {
                TB1.AppendText(sku.SkuId.ToString() + Environment.NewLine);
                TB1.AppendText(sku.SkuPartNumber + Environment.NewLine);
            }
        }
    }
}

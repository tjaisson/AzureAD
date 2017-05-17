using Microsoft.Azure.ActiveDirectory.GraphClient;
using Microsoft.Azure.ActiveDirectory.GraphClient.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AzureAD2
{
    public delegate Task UserHandler(User u);

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private async void func1()
        {
            ActiveDirectoryClient adc = await AuthenticationHelper.GetActiveDirectoryClientAsUser();
            Microsoft.Azure.ActiveDirectory.GraphClient.Extensions.IPagedCollection<IExtensionProperty> ipc =
                await adc.Applications["405622b4-e5b0-41b7-b5f1-f31cd7cf20c2"].ExtensionProperties.ExecuteAsync();
            foreach (IExtensionProperty ep in ipc.CurrentPage)
            {
                TB1.AppendText(ep.Name + Environment.NewLine);
                foreach (string tg in ep.TargetObjects)
                    TB1.AppendText(" >" + tg + Environment.NewLine);
            }
        }

        private async void func2()
        {
            ActiveDirectoryClient adc = await AuthenticationHelper.GetActiveDirectoryClientAsUser();
            IApplicationFetcher iappfetch = adc.Applications.GetByObjectId("405622b4-e5b0-41b7-b5f1-f31cd7cf20c2");
            IExtensionPropertyCollection iextpropcoll = iappfetch.ExtensionProperties;
            ExtensionProperty newProp = new ExtensionProperty
            {
                Name = "Niveau",
                DataType = "String",
                TargetObjects = { "Group" }
            };
            //await iextpropcoll.AddExtensionPropertyAsync(newProp);
            TB1.AppendText("done");
        }

        private async void func3()
        {
            ActiveDirectoryClient adc = await AuthenticationHelper.GetActiveDirectoryClientAsUser();
            IUserCollection iuc = adc.Users;
            IPagedCollection<IUser> ipc = await iuc.ExecuteAsync();

            bool encore = true;
            int i = 0;
            while (encore)
            {
                List<IUser> ul = ipc.CurrentPage.ToList();
                foreach (IUser u in ul)
                {
                    TB1.AppendText(u.DisplayName + " ; " + u.GivenName + " ; " + u.Surname + " ; " + u.UserPrincipalName + Environment.NewLine);
                    i++;
                }

                if (ipc.MorePagesAvailable)
                {
                    ipc = await ipc.GetNextPageAsync();
                }
                else
                {
                    encore = false;
                }
            }
            TB1.AppendText(Environment.NewLine + "Nombre d'utilisateurs : " + i.ToString() + Environment.NewLine);
        }

        private async void func4()
        {
            ActiveDirectoryClient adc = await AuthenticationHelper.GetActiveDirectoryClientAsUser();
            IUserCollection iuc = adc.Users;
            IPagedCollection<IUser> ipc = await iuc.ExecuteAsync();

            bool encore = true;
            int i = 0;
            while (encore)
            {
                TB1.AppendText("*****************" + Environment.NewLine);

                List<IUser> ul = ipc.CurrentPage.ToList();
                foreach (IUser u in ul)
                {
                    if (u.UserPrincipalName == "tjaisson@edu.ac-paris.fr")
                    {
                        encore = false;
                        IList<AssignedLicense> ll = u.AssignedLicenses;
                        foreach (AssignedLicense l in ll)
                        {
                            TB1.AppendText("      " + l.SkuId.ToString() + Environment.NewLine);
                            foreach (Guid gi in l.DisabledPlans)
                            {
                                TB1.AppendText("                      " + gi.ToString() + Environment.NewLine);
                            }
                        }

                    }
                    //TB1.AppendText(u.DisplayName + Environment.NewLine);
                    i++;
                }

                if (ipc.MorePagesAvailable)
                {
                    ipc = await ipc.GetNextPageAsync();
                }
                else
                {
                    encore = false;
                }

            }
            TB1.AppendText(Environment.NewLine + "Nombre d'utilisateurs : " + i.ToString() + Environment.NewLine);
        }

        private async void func5()
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

        private async void button1_Click(object sender, EventArgs e)
        {
            ActiveDirectoryClient adc = await AuthenticationHelper.GetActiveDirectoryClientAsUser();
            ISubscribedSkuCollection iskc = adc.SubscribedSkus;
            IPagedCollection<ISubscribedSku> skl = await iskc.ExecuteAsync();
            foreach (ISubscribedSku sku in skl.CurrentPage.ToList())
                if ((sku.PrepaidUnits.Enabled.Value > sku.ConsumedUnits) && (sku.CapabilityStatus == "Enabled"))
                {
                                    TB1.AppendText(Environment.NewLine + sku.SkuId.ToString() + " " + sku.SkuPartNumber + Environment.NewLine + Environment.NewLine);
                    //TB1.AppendText(Environment.NewLine + sku.SkuPartNumber + Environment.NewLine + Environment.NewLine);
                    //sku.ServicePlans 
                    IList<ServicePlanInfo> lspi = sku.ServicePlans;
                    foreach (ServicePlanInfo spi in lspi)
                    {
                                            TB1.AppendText(" > " + spi.ServicePlanName + " " + spi.ServicePlanId + Environment.NewLine);
                        //TB1.AppendText(spi.ServicePlanName + Environment.NewLine);
                    }
                }
        }

/*        
94763226-9b3c-4e75-a931-5c89701abe66 STANDARDWOFFPACK_FACULTY

 > FLOW_O365_P2 76846ad7-7776-4c40-a281-a386362dd1b9
 > POWERAPPS_O365_P2 c68f8d98-5534-41c8-bf36-22fa496fa792
 > RMS_S_ENTERPRISE bea4c11e-220a-4e6d-8eb8-8ea15d019f90
 > OFFICE_FORMS_PLAN_2 9b5de886-f035-4ff2-b3d8-c9127bea3620
 > PROJECTWORKMANAGEMENT b737dad2-2f6c-4c65-90e3-ca563267e8b9
 > SWAY a23b959c-7ce8-4e57-9140-b90eb88a9e97
 > INTUNE_O365 882e1d05-acd1-4ccb-8708-6ee03664b117
 > YAMMER_EDU 2078e8df-cff6-4290-98cb-5408261a760a
 > SHAREPOINTWAC_EDU e03c7e47-402c-463c-ab25-949079bedb21
 > MCOSTANDARD 0feaeb32-d00e-4d66-bd5a-43b5b83db82c
 > SHAREPOINTSTANDARD_EDU 0a4983bb-d3e5-4a09-95d8-b2d0127b3df5
 > EXCHANGE_S_STANDARD 9aaf7827-d63c-4b61-89c3-182f06f82e5c

314c4481-f395-4525-be8b-2ec4bb1e9d91 STANDARDWOFFPACK_STUDENT

 > FLOW_O365_P2 76846ad7-7776-4c40-a281-a386362dd1b9
 > POWERAPPS_O365_P2 c68f8d98-5534-41c8-bf36-22fa496fa792
 > RMS_S_ENTERPRISE bea4c11e-220a-4e6d-8eb8-8ea15d019f90
 > OFFICE_FORMS_PLAN_2 9b5de886-f035-4ff2-b3d8-c9127bea3620
 > PROJECTWORKMANAGEMENT b737dad2-2f6c-4c65-90e3-ca563267e8b9
 > SWAY a23b959c-7ce8-4e57-9140-b90eb88a9e97
 > INTUNE_O365 882e1d05-acd1-4ccb-8708-6ee03664b117
 > YAMMER_EDU 2078e8df-cff6-4290-98cb-5408261a760a
 > SHAREPOINTWAC_EDU e03c7e47-402c-463c-ab25-949079bedb21
 > MCOSTANDARD 0feaeb32-d00e-4d66-bd5a-43b5b83db82c
 > SHAREPOINTSTANDARD_EDU 0a4983bb-d3e5-4a09-95d8-b2d0127b3df5
 > EXCHANGE_S_STANDARD 9aaf7827-d63c-4b61-89c3-182f06f82e5c
 */


        private async Task<AssignedLicense> GetEleveLicence()
        {
            ActiveDirectoryClient adc = await AuthenticationHelper.GetActiveDirectoryClientAsUser();
            IPagedCollection<ISubscribedSku> skus = await adc.SubscribedSkus.ExecuteAsync();
            while (skus != null)
            {
                List<ISubscribedSku> subscribedSkus = skus.CurrentPage.ToList();
                foreach (ISubscribedSku sku in subscribedSkus)
                {
                    if (sku.SkuPartNumber == "STANDARDWOFFPACK_STUDENT")
                    {
                        if ((sku.PrepaidUnits.Enabled.Value > sku.ConsumedUnits) &&
                            (sku.CapabilityStatus == "Enabled"))
                        {
                            // create addLicense object and assign the Enterprise Sku GUID to the skuId
                            AssignedLicense addLicense = new AssignedLicense { SkuId = sku.SkuId.Value };

                            // find plan id of SharePoint Service Plan
                            foreach (ServicePlanInfo servicePlan in sku.ServicePlans)
                            {
                                if (
                                    //servicePlan.ServicePlanName == "EXCHANGE_S_STANDARD" || 
                                    servicePlan.ServicePlanName == "PROJECTWORKMANAGEMENT" || //planner
                                    //servicePlan.ServicePlanName == "SWAY" || 
                                    //servicePlan.ServicePlanName == "OFFICE_FORMS_PLAN_2" || //form
                                    //servicePlan.ServicePlanName == "POWERAPPS_O365_P2" || 
                                    //servicePlan.ServicePlanName == "MCOSTANDARD" ||
                                    servicePlan.ServicePlanName == "YAMMER_EDU" ||
                                    servicePlan.ServicePlanName == "FLOW_O365_P2"
                                    )
                                {
                                    addLicense.DisabledPlans.Add(servicePlan.ServicePlanId.Value);
                                }
                            }
                            return addLicense;
                        }
                    }
                }
                skus = await skus.GetNextPageAsync();
            }
            return null;
        }

        private async Task<AssignedLicense> GetprofLicence()
        {
            ActiveDirectoryClient adc = await AuthenticationHelper.GetActiveDirectoryClientAsUser();
            IPagedCollection<ISubscribedSku> skus = await adc.SubscribedSkus.ExecuteAsync();
            while (skus != null)
            {
                List<ISubscribedSku> subscribedSkus = skus.CurrentPage.ToList();
                foreach (ISubscribedSku sku in subscribedSkus)
                {
                    if (sku.SkuPartNumber == "STANDARDWOFFPACK_FACULTY")
                    {
                        if ((sku.PrepaidUnits.Enabled.Value > sku.ConsumedUnits) &&
                            (sku.CapabilityStatus == "Enabled"))
                        {
                            // create addLicense object and assign the Enterprise Sku GUID to the skuId
                            AssignedLicense addLicense = new AssignedLicense { SkuId = sku.SkuId.Value };

                            // find plan id of SharePoint Service Plan
                            foreach (ServicePlanInfo servicePlan in sku.ServicePlans)
                            {
                                if (
                                    //servicePlan.ServicePlanName == "EXCHANGE_S_STANDARD" || 
                                    servicePlan.ServicePlanName == "PROJECTWORKMANAGEMENT" || //planner
                                    //servicePlan.ServicePlanName == "SWAY" || 
                                    //servicePlan.ServicePlanName == "OFFICE_FORMS_PLAN_2" || //form
                                    //servicePlan.ServicePlanName == "POWERAPPS_O365_P2" || 
                                    //servicePlan.ServicePlanName == "MCOSTANDARD" ||
                                    servicePlan.ServicePlanName == "YAMMER_EDU" ||
                                    servicePlan.ServicePlanName == "FLOW_O365_P2"
                                    )
                                {
                                    addLicense.DisabledPlans.Add(servicePlan.ServicePlanId.Value);
                                }
                            }
                            return addLicense;
                        }
                    }
                }
                skus = await skus.GetNextPageAsync();
            }
            return null;
        }

        private async Task traiteU(User u)
        {
            if(u.UsageLocation != "FR")
            {
                u.UsageLocation = "FR";
                await u.UpdateAsync();
            }
            IList<AssignedLicense> licensesToAdd = new[] { lictoadd };
            IList<Guid> licensesToRemove = new Guid[] { };

            await u.AssignLicenseAsync(licensesToAdd, licensesToRemove);

            TB1.AppendText(" + " + u.DisplayName + Environment.NewLine);
            nbusers ++;
        }

        private int nbusers = 0;
        private AssignedLicense lictoadd;

        private async Task traiteG(IGroupFetcher igf)
        {
            IPagedCollection<IDirectoryObject> members = await igf.Members.ExecuteAsync();
            do
            {
                List<IDirectoryObject> directoryObjects = members.CurrentPage.ToList();
                foreach (IDirectoryObject member in directoryObjects)
                {
                    if (member is User)
                    {
                        User u = member as User;
                        await traiteU(u);
                    }

                    if (member is Group)
                    {
                        Group sg = member as Group;
                        TB1.AppendText("****** " + sg.DisplayName + Environment.NewLine);
                        await traiteG(sg);
                    }
                }
                members = await members.GetNextPageAsync();
            } while (members != null);
        }

        private async Task traiteG(string gn)
        {
            nbusers = 0;
            TB1.AppendText("Groupe : " + gn + Environment.NewLine + "*********************" + Environment.NewLine);
            string id = await BaseGraph.getGroupUid(gn);
            ActiveDirectoryClient adc = await AuthenticationHelper.GetActiveDirectoryClientAsUser();
            IGroupFetcher igf = adc.Groups.GetByObjectId(id);
            await traiteG(igf);
            TB1.AppendText("Nb : " + nbusers.ToString() + Environment.NewLine + Environment.NewLine);
        }

        private async void AssProfButt_Click(object sender, EventArgs e)
        {
            lictoadd = await GetprofLicence();
            await traiteG(GrpTB.Text);
        }

        private async void AssElButt_Click(object sender, EventArgs e)
        {
            lictoadd = await GetEleveLicence();
            await traiteG(GrpTB.Text);
        }

        private async Task PerformG(IGroupFetcher igf, UserHandler proc)
        {
            IPagedCollection<IDirectoryObject> members = await igf.Members.ExecuteAsync();
            do
            {
                List<IDirectoryObject> directoryObjects = members.CurrentPage.ToList();
                foreach (IDirectoryObject member in directoryObjects)
                {
                    if (member is User)
                    {
                        User u = member as User;
                        await proc(u);
                    }

                    if (member is Group)
                    {
                        Group sg = member as Group;
                        TB1.AppendText("****** " + sg.DisplayName + Environment.NewLine);
                        await PerformG(sg, proc);
                    }
                }
                members = await members.GetNextPageAsync();
            } while (members != null);
        }

        private async Task PerformG(string gn, UserHandler proc)
        {
            nbusers = 0;
            TB1.AppendText("Groupe : " + gn + Environment.NewLine + "*********************" + Environment.NewLine);
            string id = await BaseGraph.getGroupUid(gn);
            ActiveDirectoryClient adc = await AuthenticationHelper.GetActiveDirectoryClientAsUser();
            IGroupFetcher igf = adc.Groups.GetByObjectId(id);
            await PerformG(igf, proc);
            TB1.AppendText("Nb : " + nbusers.ToString() + Environment.NewLine + Environment.NewLine);
        }


        private async Task UProc3(User u)
        {
            TB1.AppendText("Start ");
            u.PhysicalDeliveryOfficeName = null;
            await u.UpdateAsync();
            TB1.AppendText(u.DisplayName + Environment.NewLine);
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            await PerformG(GrpTB.Text, UProc3);
        }


        private async Task UProc4(User u)
        {
            if (u.DirSyncEnabled == true)
                TB1.AppendText(u.DisplayName + " " + u.PhysicalDeliveryOfficeName + " " + u.PostalCode + Environment.NewLine);
        }


        private async void button4_Click(object sender, EventArgs e)
        {
            await PerformG(GrpTB.Text, UProc4);
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            string id = await BaseGraph.getUserUid("test.tj@edu-ms.ac-paris.fr");
            ActiveDirectoryClient adc = await AuthenticationHelper.GetActiveDirectoryClientAsUser();
            IUserFetcher iuf = adc.Users.GetByObjectId(id);
            IUser iu = await iuf.ExecuteAsync();
            /*
            IList<AssignedLicense> ial = iu.AssignedLicenses;
            foreach(AssignedLicense al in ial)
            {
                TB1.AppendText(al.SkuId + Environment.NewLine);
                foreach(Guid gd in al.DisabledPlans)
                {
                    TB1.AppendText(" " + gd + Environment.NewLine);
                }
            }
            foreach (AssignedPlan ap in iu.AssignedPlans)
            {
                TB1.AppendText(" " + ap.Service +" " + ap.ServicePlanId + Environment.NewLine);
            }
            */
            iu.PhysicalDeliveryOfficeName = null;
            await iu.UpdateAsync();
            TB1.AppendText(" done " + Environment.NewLine);
        }

        private void RstButt_Click(object sender, EventArgs e)
        {
            AuthenticationHelper.DoReset = true;
        }

        private async void AssignUserbutt_Click(object sender, EventArgs e)
        {
            lictoadd = await GetEleveLicence();
            string id = await BaseGraph.getUserUid(GrpTB.Text);
            ActiveDirectoryClient adc = await AuthenticationHelper.GetActiveDirectoryClientAsUser();
            IUserFetcher iuf = adc.Users.GetByObjectId(id);
            IUser u = await iuf.ExecuteAsync();
            if (u.UsageLocation != "FR")
            {
                u.UsageLocation = "FR";
                await u.UpdateAsync();
            }
            IList<AssignedLicense> licensesToAdd = new[] { lictoadd };
            IList<Guid> licensesToRemove = new Guid[] { };

            await u.AssignLicenseAsync(licensesToAdd, licensesToRemove);
            TB1.AppendText("effectué" + Environment.NewLine);
        }
    }
}

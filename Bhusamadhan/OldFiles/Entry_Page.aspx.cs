using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bhusamadhan.LandDispute.Entry
{
    public partial class Entry_Page : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowStep(CurrentStep);
            }
        }

     
        public int CurrentStep
        {
            get
            {
                if (ViewState["CurrentStep"] == null)
                    ViewState["CurrentStep"] = 1;

                return Convert.ToInt32(ViewState["CurrentStep"]);
            }

            set
            {
                ViewState["CurrentStep"] = value;
            }
        }

        private void ShowStep(int step)
        {
            pnlStep1.Visible = false;
            pnlStep2.Visible = false;
            pnlStep3.Visible = false;
            pnlStep4.Visible = false;
            pnlStep5.Visible = false;
            pnlStep6.Visible = false;
            pnlStep7.Visible = false;

            switch (step)
            {
                case 1:
                    pnlStep1.Visible = true;
                    break;

                case 2:
                    pnlStep2.Visible = true;
                    break;

                case 3:
                    pnlStep3.Visible = true;
                    break;

                case 4:
                    pnlStep4.Visible = true;
                    break;

                case 5:
                    pnlStep5.Visible = true;
                    break;

                case 6:
                    pnlStep6.Visible = true;
                    break;

                case 7:
                    pnlStep7.Visible = true;
                    break;
            }

            SetWizard(step);

            btnPrevious.Visible = (step > 1);

            //btnNext.Text = (step == 7) ? "Finish" : "Save & Next";
            if (step == 7)
                btnNext.Text = "Finish";
            else
                btnNext.Text = "Save & Next";
        }

        private void SetWizard(int currentStep)
        {
            System.Web.UI.HtmlControls.HtmlAnchor[] steps =
            {
                    hstep1, hstep2, hstep3, hstep4, hstep5, hstep6, hstep7
            };

            for (int i = 0; i < steps.Length; i++)
            {
                if (i < currentStep - 1)
                {
                    steps[i].Attributes["class"] = "step completed";
                }
                else if (i == currentStep - 1)
                {
                    steps[i].Attributes["class"] = "step current";
                }
                else
                {
                    steps[i].Attributes["class"] = "step disabled";
                }
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            // Save Current Step Data Here

            if (CurrentStep < 7)
            {
                CurrentStep++;
            }

            ShowStep(CurrentStep);
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            if (CurrentStep > 1)
            {
                CurrentStep--;
            }

            ShowStep(CurrentStep);
        }

    }
}
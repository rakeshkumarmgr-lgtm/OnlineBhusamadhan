<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddHoliday.aspx.cs" Inherits="Bhusamadhan.LandDispute.Entry.AddHoliday" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .holiday-card {
            margin-top: 20px;
            border: none;
            box-shadow: 0 3px 6px rgba(0, 0, 0, 0.16), 0 3px 6px rgba(0, 0, 0, 0.23);
        }

        .holiday-header {
            background: linear-gradient(90deg, #3aa7a3, #6b6fae);
            color: #fff;
            text-align: center;
            padding: 10px;
        }

            .holiday-header h4 {
                margin: 0;
                font-size: 20px;
                font-weight: 500;
            }

        .label-custom {
            display: block;
            margin-bottom: 6px;
            font-weight: 500;
        }

        .btn-add-holiday {
            min-width: 110px;
            background: linear-gradient(90deg, #3aa7a3, #6b6fae);
            border: none;
            color: #fff;
            font-weight: 500;
            padding: 7px 18px;
        }

            .btn-add-holiday:hover {
                color: #fff;
                opacity: 0.9;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <div class="row justify-content-center">

        <div class="col-md-12">
            <asp:Label ID="lblMsg" runat="server" CssClass="font-weight-bold text-danger">  </asp:Label>
        </div>
        <div class="col-md-6">

            <div class="card holiday-card">

                <div class="card-header holiday-header">
                    <h4>Add Holiday</h4>
                </div>

                <div class="card-body">

                    <div class="form-group">
                        <label class="label-custom">Holiday Date </label>

                        <asp:TextBox ID="txtHolidayDate" runat="server" CssClass="form-control" TextMode="Date" />
                        <asp:RequiredFieldValidator ID="rfv1" runat="server" ForeColor="Red" ControlToValidate="txtHolidayDate" ErrorMessage="required" ValidationGroup="1" Display="Dynamic" SetFocusOnError="true" />
                    </div>

                    <div class="form-group mt-3">
                        <label class="label-custom">Reason for Holiday </label>

                        <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" Placeholder="Reason for Holiday" />
                        <asp:RequiredFieldValidator ID="rfv2" runat="server" ForeColor="Red" ControlToValidate="txtRemark" ErrorMessage="required" ValidationGroup="1" Display="Dynamic" SetFocusOnError="true" />
                    </div>

                    <div class="text-center mt-4">
                        <asp:Button ID="btnSend" runat="server" Text="Add Holiday" CssClass="btn btn-add-holiday" ValidationGroup="1" OnClick="btnSend_Click" />
                    </div>

                </div>
            </div>

        </div>
    </div>
</asp:Content>

Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SqlClient
Imports System.IO
Public Class FrmItemMaster
    Inherits AgTemplate.TempMaster
    Dim mQry$
    Friend WithEvents ChkIsSystemDefine As System.Windows.Forms.CheckBox
    Public WithEvents LblIsSystemDefine As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents TxtStdWeight As AgControls.AgTextBox
    Dim Photo_Byte As Byte()

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)
    End Sub

#Region "Designer Code"
    Private Sub InitializeComponent()
        Me.TxtCustomFields = New AgControls.AgTextBox
        Me.PicPhoto = New System.Windows.Forms.PictureBox
        Me.BtnBrowse = New System.Windows.Forms.Button
        Me.BtnPhotoClear = New System.Windows.Forms.Button
        Me.TxtPurchQtyAllowedWithoutPO = New AgControls.AgTextBox
        Me.Label21 = New System.Windows.Forms.Label
        Me.TxtIsRequired_LotNo = New AgControls.AgTextBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.TxtBinLocation = New AgControls.AgTextBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.TxtMaximumStockLevel = New AgControls.AgTextBox
        Me.TxtReOrderStockLevel = New AgControls.AgTextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.TxtMinimumStockLevel = New AgControls.AgTextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.TxtTariffHead = New AgControls.AgTextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.LblItemCategory = New System.Windows.Forms.Label
        Me.TxtVatCommodity = New AgControls.AgTextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.PnlCustomGrid = New System.Windows.Forms.Panel
        Me.TxtItemInvoiceGroup = New AgControls.AgTextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.TxtSpecification = New AgControls.AgTextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.TxtMeasurePerPcs = New AgControls.AgTextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.TxtMeasureUnit = New AgControls.AgTextBox
        Me.LblMeasureUnit = New System.Windows.Forms.Label
        Me.TxtItemType = New AgControls.AgTextBox
        Me.TxtRate = New AgControls.AgTextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.TxtItemCategory = New AgControls.AgTextBox
        Me.TxtItemGroup = New AgControls.AgTextBox
        Me.LblItemGroup = New System.Windows.Forms.Label
        Me.TxtSalesTaxPostingGroup = New AgControls.AgTextBox
        Me.LblSalesTaxPostingGroup = New System.Windows.Forms.Label
        Me.LblManualCodeReq = New System.Windows.Forms.Label
        Me.TxtManualCode = New AgControls.AgTextBox
        Me.LblManualCode = New System.Windows.Forms.Label
        Me.TxtUnit = New AgControls.AgTextBox
        Me.LblUnit = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.TxtDescription = New AgControls.AgTextBox
        Me.LblDescription = New System.Windows.Forms.Label
        Me.GBOtherDetails = New System.Windows.Forms.GroupBox
        Me.TxtIsRestrictedinTransaction = New AgControls.AgTextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.TxtIsUnitConversionMandatory = New AgControls.AgTextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.LblMaterialPlanForFollowingItems = New System.Windows.Forms.LinkLabel
        Me.BtnUnitConversion = New System.Windows.Forms.Button
        Me.BtnBOMDetail = New System.Windows.Forms.Button
        Me.ChkIsSystemDefine = New System.Windows.Forms.CheckBox
        Me.LblIsSystemDefine = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.TxtStdWeight = New AgControls.AgTextBox
        Me.GrpUP.SuspendLayout()
        Me.GBoxEntryType.SuspendLayout()
        Me.GBoxMoveToLog.SuspendLayout()
        Me.GBoxApprove.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GBoxDivision.SuspendLayout()
        CType(Me.DTMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicPhoto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GBOtherDetails.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(944, 41)
        Me.Topctrl1.tAdd = False
        Me.Topctrl1.tDel = False
        Me.Topctrl1.tEdit = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(0, 444)
        Me.GroupBox1.Size = New System.Drawing.Size(986, 4)
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(14, 448)
        '
        'TxtEntryBy
        '
        Me.TxtEntryBy.Tag = ""
        Me.TxtEntryBy.Text = ""
        '
        'GBoxEntryType
        '
        Me.GBoxEntryType.Location = New System.Drawing.Point(148, 448)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Tag = ""
        '
        'GBoxMoveToLog
        '
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(554, 448)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Tag = ""
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(401, 448)
        Me.GBoxApprove.Text = "Approved By"
        '
        'TxtApproveBy
        '
        Me.TxtApproveBy.Location = New System.Drawing.Point(3, 23)
        Me.TxtApproveBy.Size = New System.Drawing.Size(136, 18)
        Me.TxtApproveBy.Tag = ""
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(704, 448)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(278, 448)
        '
        'TxtDivision
        '
        Me.TxtDivision.AgSelectedValue = ""
        Me.TxtDivision.Tag = ""
        '
        'TxtStatus
        '
        Me.TxtStatus.AgSelectedValue = ""
        Me.TxtStatus.Tag = ""
        '
        'TxtCustomFields
        '
        Me.TxtCustomFields.AgAllowUserToEnableMasterHelp = False
        Me.TxtCustomFields.AgLastValueTag = Nothing
        Me.TxtCustomFields.AgLastValueText = Nothing
        Me.TxtCustomFields.AgMandatory = False
        Me.TxtCustomFields.AgMasterHelp = False
        Me.TxtCustomFields.AgNumberLeftPlaces = 8
        Me.TxtCustomFields.AgNumberNegetiveAllow = False
        Me.TxtCustomFields.AgNumberRightPlaces = 2
        Me.TxtCustomFields.AgPickFromLastValue = False
        Me.TxtCustomFields.AgRowFilter = ""
        Me.TxtCustomFields.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCustomFields.AgSelectedValue = Nothing
        Me.TxtCustomFields.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCustomFields.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtCustomFields.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtCustomFields.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCustomFields.Location = New System.Drawing.Point(473, 214)
        Me.TxtCustomFields.MaxLength = 20
        Me.TxtCustomFields.Name = "TxtCustomFields"
        Me.TxtCustomFields.Size = New System.Drawing.Size(72, 18)
        Me.TxtCustomFields.TabIndex = 2
        Me.TxtCustomFields.Text = "AgTextBox1"
        Me.TxtCustomFields.Visible = False
        '
        'PicPhoto
        '
        Me.PicPhoto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PicPhoto.Location = New System.Drawing.Point(6, 31)
        Me.PicPhoto.Name = "PicPhoto"
        Me.PicPhoto.Size = New System.Drawing.Size(155, 129)
        Me.PicPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PicPhoto.TabIndex = 1015
        Me.PicPhoto.TabStop = False
        '
        'BtnBrowse
        '
        Me.BtnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnBrowse.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnBrowse.Location = New System.Drawing.Point(6, 164)
        Me.BtnBrowse.Name = "BtnBrowse"
        Me.BtnBrowse.Size = New System.Drawing.Size(69, 23)
        Me.BtnBrowse.TabIndex = 20
        Me.BtnBrowse.Text = "Browse"
        Me.BtnBrowse.UseVisualStyleBackColor = True
        '
        'BtnPhotoClear
        '
        Me.BtnPhotoClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnPhotoClear.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPhotoClear.Location = New System.Drawing.Point(91, 164)
        Me.BtnPhotoClear.Name = "BtnPhotoClear"
        Me.BtnPhotoClear.Size = New System.Drawing.Size(69, 23)
        Me.BtnPhotoClear.TabIndex = 21
        Me.BtnPhotoClear.Text = "Clear"
        Me.BtnPhotoClear.UseVisualStyleBackColor = True
        '
        'TxtPurchQtyAllowedWithoutPO
        '
        Me.TxtPurchQtyAllowedWithoutPO.AgAllowUserToEnableMasterHelp = False
        Me.TxtPurchQtyAllowedWithoutPO.AgLastValueTag = Nothing
        Me.TxtPurchQtyAllowedWithoutPO.AgLastValueText = Nothing
        Me.TxtPurchQtyAllowedWithoutPO.AgMandatory = False
        Me.TxtPurchQtyAllowedWithoutPO.AgMasterHelp = False
        Me.TxtPurchQtyAllowedWithoutPO.AgNumberLeftPlaces = 6
        Me.TxtPurchQtyAllowedWithoutPO.AgNumberNegetiveAllow = False
        Me.TxtPurchQtyAllowedWithoutPO.AgNumberRightPlaces = 3
        Me.TxtPurchQtyAllowedWithoutPO.AgPickFromLastValue = False
        Me.TxtPurchQtyAllowedWithoutPO.AgRowFilter = ""
        Me.TxtPurchQtyAllowedWithoutPO.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtPurchQtyAllowedWithoutPO.AgSelectedValue = Nothing
        Me.TxtPurchQtyAllowedWithoutPO.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPurchQtyAllowedWithoutPO.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtPurchQtyAllowedWithoutPO.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPurchQtyAllowedWithoutPO.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPurchQtyAllowedWithoutPO.Location = New System.Drawing.Point(450, 45)
        Me.TxtPurchQtyAllowedWithoutPO.MaxLength = 10
        Me.TxtPurchQtyAllowedWithoutPO.Name = "TxtPurchQtyAllowedWithoutPO"
        Me.TxtPurchQtyAllowedWithoutPO.Size = New System.Drawing.Size(80, 18)
        Me.TxtPurchQtyAllowedWithoutPO.TabIndex = 3
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(256, 47)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(191, 16)
        Me.Label21.TabIndex = 752
        Me.Label21.Text = "Purch. Qty Allowed Without PO"
        '
        'TxtIsRequired_LotNo
        '
        Me.TxtIsRequired_LotNo.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsRequired_LotNo.AgLastValueTag = Nothing
        Me.TxtIsRequired_LotNo.AgLastValueText = Nothing
        Me.TxtIsRequired_LotNo.AgMandatory = False
        Me.TxtIsRequired_LotNo.AgMasterHelp = False
        Me.TxtIsRequired_LotNo.AgNumberLeftPlaces = 0
        Me.TxtIsRequired_LotNo.AgNumberNegetiveAllow = False
        Me.TxtIsRequired_LotNo.AgNumberRightPlaces = 0
        Me.TxtIsRequired_LotNo.AgPickFromLastValue = False
        Me.TxtIsRequired_LotNo.AgRowFilter = ""
        Me.TxtIsRequired_LotNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsRequired_LotNo.AgSelectedValue = Nothing
        Me.TxtIsRequired_LotNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsRequired_LotNo.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsRequired_LotNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsRequired_LotNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsRequired_LotNo.Location = New System.Drawing.Point(174, 47)
        Me.TxtIsRequired_LotNo.MaxLength = 20
        Me.TxtIsRequired_LotNo.Name = "TxtIsRequired_LotNo"
        Me.TxtIsRequired_LotNo.Size = New System.Drawing.Size(79, 18)
        Me.TxtIsRequired_LotNo.TabIndex = 2
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(9, 49)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(115, 16)
        Me.Label20.TabIndex = 751
        Me.Label20.Text = "Is Required Lot No"
        '
        'TxtBinLocation
        '
        Me.TxtBinLocation.AgAllowUserToEnableMasterHelp = False
        Me.TxtBinLocation.AgLastValueTag = Nothing
        Me.TxtBinLocation.AgLastValueText = Nothing
        Me.TxtBinLocation.AgMandatory = False
        Me.TxtBinLocation.AgMasterHelp = False
        Me.TxtBinLocation.AgNumberLeftPlaces = 0
        Me.TxtBinLocation.AgNumberNegetiveAllow = False
        Me.TxtBinLocation.AgNumberRightPlaces = 0
        Me.TxtBinLocation.AgPickFromLastValue = False
        Me.TxtBinLocation.AgRowFilter = ""
        Me.TxtBinLocation.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtBinLocation.AgSelectedValue = Nothing
        Me.TxtBinLocation.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtBinLocation.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtBinLocation.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtBinLocation.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBinLocation.Location = New System.Drawing.Point(450, 85)
        Me.TxtBinLocation.MaxLength = 20
        Me.TxtBinLocation.Name = "TxtBinLocation"
        Me.TxtBinLocation.Size = New System.Drawing.Size(80, 18)
        Me.TxtBinLocation.TabIndex = 7
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(256, 87)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(80, 16)
        Me.Label18.TabIndex = 750
        Me.Label18.Text = "Bin Location"
        '
        'TxtMaximumStockLevel
        '
        Me.TxtMaximumStockLevel.AgAllowUserToEnableMasterHelp = False
        Me.TxtMaximumStockLevel.AgLastValueTag = Nothing
        Me.TxtMaximumStockLevel.AgLastValueText = Nothing
        Me.TxtMaximumStockLevel.AgMandatory = False
        Me.TxtMaximumStockLevel.AgMasterHelp = False
        Me.TxtMaximumStockLevel.AgNumberLeftPlaces = 6
        Me.TxtMaximumStockLevel.AgNumberNegetiveAllow = False
        Me.TxtMaximumStockLevel.AgNumberRightPlaces = 3
        Me.TxtMaximumStockLevel.AgPickFromLastValue = False
        Me.TxtMaximumStockLevel.AgRowFilter = ""
        Me.TxtMaximumStockLevel.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtMaximumStockLevel.AgSelectedValue = Nothing
        Me.TxtMaximumStockLevel.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtMaximumStockLevel.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtMaximumStockLevel.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtMaximumStockLevel.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMaximumStockLevel.Location = New System.Drawing.Point(450, 65)
        Me.TxtMaximumStockLevel.MaxLength = 10
        Me.TxtMaximumStockLevel.Name = "TxtMaximumStockLevel"
        Me.TxtMaximumStockLevel.Size = New System.Drawing.Size(80, 18)
        Me.TxtMaximumStockLevel.TabIndex = 5
        '
        'TxtReOrderStockLevel
        '
        Me.TxtReOrderStockLevel.AgAllowUserToEnableMasterHelp = False
        Me.TxtReOrderStockLevel.AgLastValueTag = Nothing
        Me.TxtReOrderStockLevel.AgLastValueText = Nothing
        Me.TxtReOrderStockLevel.AgMandatory = False
        Me.TxtReOrderStockLevel.AgMasterHelp = False
        Me.TxtReOrderStockLevel.AgNumberLeftPlaces = 6
        Me.TxtReOrderStockLevel.AgNumberNegetiveAllow = False
        Me.TxtReOrderStockLevel.AgNumberRightPlaces = 3
        Me.TxtReOrderStockLevel.AgPickFromLastValue = False
        Me.TxtReOrderStockLevel.AgRowFilter = ""
        Me.TxtReOrderStockLevel.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtReOrderStockLevel.AgSelectedValue = Nothing
        Me.TxtReOrderStockLevel.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtReOrderStockLevel.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtReOrderStockLevel.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtReOrderStockLevel.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtReOrderStockLevel.Location = New System.Drawing.Point(174, 87)
        Me.TxtReOrderStockLevel.MaxLength = 10
        Me.TxtReOrderStockLevel.Name = "TxtReOrderStockLevel"
        Me.TxtReOrderStockLevel.Size = New System.Drawing.Size(79, 18)
        Me.TxtReOrderStockLevel.TabIndex = 6
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(9, 67)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(99, 16)
        Me.Label17.TabIndex = 747
        Me.Label17.Text = "Minimum Stock"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(9, 87)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(98, 16)
        Me.Label19.TabIndex = 749
        Me.Label19.Text = "Re-Order Stock"
        '
        'TxtMinimumStockLevel
        '
        Me.TxtMinimumStockLevel.AgAllowUserToEnableMasterHelp = False
        Me.TxtMinimumStockLevel.AgLastValueTag = Nothing
        Me.TxtMinimumStockLevel.AgLastValueText = "0.000"
        Me.TxtMinimumStockLevel.AgMandatory = False
        Me.TxtMinimumStockLevel.AgMasterHelp = False
        Me.TxtMinimumStockLevel.AgNumberLeftPlaces = 6
        Me.TxtMinimumStockLevel.AgNumberNegetiveAllow = False
        Me.TxtMinimumStockLevel.AgNumberRightPlaces = 3
        Me.TxtMinimumStockLevel.AgPickFromLastValue = False
        Me.TxtMinimumStockLevel.AgRowFilter = ""
        Me.TxtMinimumStockLevel.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtMinimumStockLevel.AgSelectedValue = Nothing
        Me.TxtMinimumStockLevel.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtMinimumStockLevel.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtMinimumStockLevel.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtMinimumStockLevel.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMinimumStockLevel.Location = New System.Drawing.Point(174, 67)
        Me.TxtMinimumStockLevel.MaxLength = 10
        Me.TxtMinimumStockLevel.Name = "TxtMinimumStockLevel"
        Me.TxtMinimumStockLevel.Size = New System.Drawing.Size(79, 18)
        Me.TxtMinimumStockLevel.TabIndex = 4
        Me.TxtMinimumStockLevel.Text = "0.000"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(256, 67)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(103, 16)
        Me.Label16.TabIndex = 748
        Me.Label16.Text = "Maximum Stock"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(270, 136)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(64, 16)
        Me.Label15.TabIndex = 1056
        Me.Label15.Text = "Item Type"
        '
        'TxtTariffHead
        '
        Me.TxtTariffHead.AgAllowUserToEnableMasterHelp = False
        Me.TxtTariffHead.AgLastValueTag = Nothing
        Me.TxtTariffHead.AgLastValueText = Nothing
        Me.TxtTariffHead.AgMandatory = False
        Me.TxtTariffHead.AgMasterHelp = False
        Me.TxtTariffHead.AgNumberLeftPlaces = 0
        Me.TxtTariffHead.AgNumberNegetiveAllow = False
        Me.TxtTariffHead.AgNumberRightPlaces = 0
        Me.TxtTariffHead.AgPickFromLastValue = False
        Me.TxtTariffHead.AgRowFilter = ""
        Me.TxtTariffHead.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtTariffHead.AgSelectedValue = Nothing
        Me.TxtTariffHead.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtTariffHead.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtTariffHead.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtTariffHead.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTariffHead.Location = New System.Drawing.Point(400, 176)
        Me.TxtTariffHead.MaxLength = 20
        Me.TxtTariffHead.Name = "TxtTariffHead"
        Me.TxtTariffHead.Size = New System.Drawing.Size(145, 18)
        Me.TxtTariffHead.TabIndex = 13
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(270, 176)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(85, 16)
        Me.Label2.TabIndex = 1055
        Me.Label2.Text = "Tariff Heading"
        '
        'LblItemCategory
        '
        Me.LblItemCategory.AutoSize = True
        Me.LblItemCategory.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblItemCategory.Location = New System.Drawing.Point(9, 156)
        Me.LblItemCategory.Name = "LblItemCategory"
        Me.LblItemCategory.Size = New System.Drawing.Size(89, 16)
        Me.LblItemCategory.TabIndex = 1054
        Me.LblItemCategory.Text = "Item Category"
        '
        'TxtVatCommodity
        '
        Me.TxtVatCommodity.AgAllowUserToEnableMasterHelp = False
        Me.TxtVatCommodity.AgLastValueTag = Nothing
        Me.TxtVatCommodity.AgLastValueText = Nothing
        Me.TxtVatCommodity.AgMandatory = False
        Me.TxtVatCommodity.AgMasterHelp = False
        Me.TxtVatCommodity.AgNumberLeftPlaces = 0
        Me.TxtVatCommodity.AgNumberNegetiveAllow = False
        Me.TxtVatCommodity.AgNumberRightPlaces = 0
        Me.TxtVatCommodity.AgPickFromLastValue = False
        Me.TxtVatCommodity.AgRowFilter = ""
        Me.TxtVatCommodity.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtVatCommodity.AgSelectedValue = Nothing
        Me.TxtVatCommodity.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtVatCommodity.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtVatCommodity.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtVatCommodity.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtVatCommodity.Location = New System.Drawing.Point(400, 156)
        Me.TxtVatCommodity.MaxLength = 20
        Me.TxtVatCommodity.Name = "TxtVatCommodity"
        Me.TxtVatCommodity.Size = New System.Drawing.Size(145, 18)
        Me.TxtVatCommodity.TabIndex = 11
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(270, 156)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(97, 16)
        Me.Label14.TabIndex = 1053
        Me.Label14.Text = "Vat Commodity"
        '
        'PnlCustomGrid
        '
        Me.PnlCustomGrid.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlCustomGrid.Location = New System.Drawing.Point(572, 46)
        Me.PnlCustomGrid.Name = "PnlCustomGrid"
        Me.PnlCustomGrid.Size = New System.Drawing.Size(342, 192)
        Me.PnlCustomGrid.TabIndex = 17
        '
        'TxtItemInvoiceGroup
        '
        Me.TxtItemInvoiceGroup.AgAllowUserToEnableMasterHelp = False
        Me.TxtItemInvoiceGroup.AgLastValueTag = Nothing
        Me.TxtItemInvoiceGroup.AgLastValueText = Nothing
        Me.TxtItemInvoiceGroup.AgMandatory = False
        Me.TxtItemInvoiceGroup.AgMasterHelp = False
        Me.TxtItemInvoiceGroup.AgNumberLeftPlaces = 0
        Me.TxtItemInvoiceGroup.AgNumberNegetiveAllow = False
        Me.TxtItemInvoiceGroup.AgNumberRightPlaces = 0
        Me.TxtItemInvoiceGroup.AgPickFromLastValue = False
        Me.TxtItemInvoiceGroup.AgRowFilter = ""
        Me.TxtItemInvoiceGroup.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtItemInvoiceGroup.AgSelectedValue = Nothing
        Me.TxtItemInvoiceGroup.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtItemInvoiceGroup.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtItemInvoiceGroup.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtItemInvoiceGroup.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtItemInvoiceGroup.Location = New System.Drawing.Point(131, 196)
        Me.TxtItemInvoiceGroup.MaxLength = 20
        Me.TxtItemInvoiceGroup.Name = "TxtItemInvoiceGroup"
        Me.TxtItemInvoiceGroup.Size = New System.Drawing.Size(414, 18)
        Me.TxtItemInvoiceGroup.TabIndex = 14
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(9, 196)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(115, 16)
        Me.Label9.TabIndex = 1052
        Me.Label9.Text = "Item Invoice Group"
        '
        'TxtSpecification
        '
        Me.TxtSpecification.AgAllowUserToEnableMasterHelp = False
        Me.TxtSpecification.AgLastValueTag = Nothing
        Me.TxtSpecification.AgLastValueText = Nothing
        Me.TxtSpecification.AgMandatory = False
        Me.TxtSpecification.AgMasterHelp = False
        Me.TxtSpecification.AgNumberLeftPlaces = 0
        Me.TxtSpecification.AgNumberNegetiveAllow = False
        Me.TxtSpecification.AgNumberRightPlaces = 0
        Me.TxtSpecification.AgPickFromLastValue = False
        Me.TxtSpecification.AgRowFilter = ""
        Me.TxtSpecification.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtSpecification.AgSelectedValue = Nothing
        Me.TxtSpecification.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtSpecification.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtSpecification.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtSpecification.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSpecification.Location = New System.Drawing.Point(131, 216)
        Me.TxtSpecification.MaxLength = 20
        Me.TxtSpecification.Name = "TxtSpecification"
        Me.TxtSpecification.Size = New System.Drawing.Size(131, 18)
        Me.TxtSpecification.TabIndex = 15
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(9, 216)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(82, 16)
        Me.Label10.TabIndex = 1051
        Me.Label10.Text = "Specification"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(115, 138)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(10, 7)
        Me.Label6.TabIndex = 1049
        Me.Label6.Text = "Ä"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label8.Location = New System.Drawing.Point(115, 178)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(10, 7)
        Me.Label8.TabIndex = 1048
        Me.Label8.Text = "Ä"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(115, 98)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(10, 7)
        Me.Label5.TabIndex = 1047
        Me.Label5.Text = "Ä"
        '
        'TxtMeasurePerPcs
        '
        Me.TxtMeasurePerPcs.AgAllowUserToEnableMasterHelp = False
        Me.TxtMeasurePerPcs.AgLastValueTag = Nothing
        Me.TxtMeasurePerPcs.AgLastValueText = Nothing
        Me.TxtMeasurePerPcs.AgMandatory = False
        Me.TxtMeasurePerPcs.AgMasterHelp = False
        Me.TxtMeasurePerPcs.AgNumberLeftPlaces = 0
        Me.TxtMeasurePerPcs.AgNumberNegetiveAllow = False
        Me.TxtMeasurePerPcs.AgNumberRightPlaces = 0
        Me.TxtMeasurePerPcs.AgPickFromLastValue = False
        Me.TxtMeasurePerPcs.AgRowFilter = ""
        Me.TxtMeasurePerPcs.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtMeasurePerPcs.AgSelectedValue = Nothing
        Me.TxtMeasurePerPcs.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtMeasurePerPcs.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtMeasurePerPcs.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtMeasurePerPcs.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMeasurePerPcs.Location = New System.Drawing.Point(131, 116)
        Me.TxtMeasurePerPcs.MaxLength = 20
        Me.TxtMeasurePerPcs.Name = "TxtMeasurePerPcs"
        Me.TxtMeasurePerPcs.Size = New System.Drawing.Size(131, 18)
        Me.TxtMeasurePerPcs.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(9, 116)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(107, 16)
        Me.Label4.TabIndex = 1046
        Me.Label4.Text = "Measure Per Qty"
        '
        'TxtMeasureUnit
        '
        Me.TxtMeasureUnit.AgAllowUserToEnableMasterHelp = False
        Me.TxtMeasureUnit.AgLastValueTag = Nothing
        Me.TxtMeasureUnit.AgLastValueText = Nothing
        Me.TxtMeasureUnit.AgMandatory = False
        Me.TxtMeasureUnit.AgMasterHelp = False
        Me.TxtMeasureUnit.AgNumberLeftPlaces = 0
        Me.TxtMeasureUnit.AgNumberNegetiveAllow = False
        Me.TxtMeasureUnit.AgNumberRightPlaces = 0
        Me.TxtMeasureUnit.AgPickFromLastValue = False
        Me.TxtMeasureUnit.AgRowFilter = ""
        Me.TxtMeasureUnit.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtMeasureUnit.AgSelectedValue = Nothing
        Me.TxtMeasureUnit.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtMeasureUnit.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtMeasureUnit.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtMeasureUnit.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMeasureUnit.Location = New System.Drawing.Point(400, 96)
        Me.TxtMeasureUnit.MaxLength = 20
        Me.TxtMeasureUnit.Name = "TxtMeasureUnit"
        Me.TxtMeasureUnit.Size = New System.Drawing.Size(145, 18)
        Me.TxtMeasureUnit.TabIndex = 5
        '
        'LblMeasureUnit
        '
        Me.LblMeasureUnit.AutoSize = True
        Me.LblMeasureUnit.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMeasureUnit.Location = New System.Drawing.Point(270, 96)
        Me.LblMeasureUnit.Name = "LblMeasureUnit"
        Me.LblMeasureUnit.Size = New System.Drawing.Size(85, 16)
        Me.LblMeasureUnit.TabIndex = 1045
        Me.LblMeasureUnit.Text = "Measure Unit"
        '
        'TxtItemType
        '
        Me.TxtItemType.AgAllowUserToEnableMasterHelp = False
        Me.TxtItemType.AgLastValueTag = Nothing
        Me.TxtItemType.AgLastValueText = Nothing
        Me.TxtItemType.AgMandatory = False
        Me.TxtItemType.AgMasterHelp = True
        Me.TxtItemType.AgNumberLeftPlaces = 0
        Me.TxtItemType.AgNumberNegetiveAllow = False
        Me.TxtItemType.AgNumberRightPlaces = 0
        Me.TxtItemType.AgPickFromLastValue = False
        Me.TxtItemType.AgRowFilter = ""
        Me.TxtItemType.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtItemType.AgSelectedValue = Nothing
        Me.TxtItemType.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtItemType.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtItemType.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtItemType.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtItemType.Location = New System.Drawing.Point(400, 136)
        Me.TxtItemType.MaxLength = 20
        Me.TxtItemType.Name = "TxtItemType"
        Me.TxtItemType.Size = New System.Drawing.Size(145, 18)
        Me.TxtItemType.TabIndex = 9
        '
        'TxtRate
        '
        Me.TxtRate.AgAllowUserToEnableMasterHelp = False
        Me.TxtRate.AgLastValueTag = Nothing
        Me.TxtRate.AgLastValueText = Nothing
        Me.TxtRate.AgMandatory = False
        Me.TxtRate.AgMasterHelp = False
        Me.TxtRate.AgNumberLeftPlaces = 0
        Me.TxtRate.AgNumberNegetiveAllow = False
        Me.TxtRate.AgNumberRightPlaces = 0
        Me.TxtRate.AgPickFromLastValue = False
        Me.TxtRate.AgRowFilter = ""
        Me.TxtRate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtRate.AgSelectedValue = Nothing
        Me.TxtRate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtRate.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtRate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtRate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRate.Location = New System.Drawing.Point(400, 116)
        Me.TxtRate.MaxLength = 20
        Me.TxtRate.Name = "TxtRate"
        Me.TxtRate.Size = New System.Drawing.Size(145, 18)
        Me.TxtRate.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(270, 116)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 16)
        Me.Label3.TabIndex = 1043
        Me.Label3.Text = "Rate"
        '
        'TxtItemCategory
        '
        Me.TxtItemCategory.AgAllowUserToEnableMasterHelp = False
        Me.TxtItemCategory.AgLastValueTag = Nothing
        Me.TxtItemCategory.AgLastValueText = Nothing
        Me.TxtItemCategory.AgMandatory = False
        Me.TxtItemCategory.AgMasterHelp = False
        Me.TxtItemCategory.AgNumberLeftPlaces = 0
        Me.TxtItemCategory.AgNumberNegetiveAllow = False
        Me.TxtItemCategory.AgNumberRightPlaces = 0
        Me.TxtItemCategory.AgPickFromLastValue = False
        Me.TxtItemCategory.AgRowFilter = ""
        Me.TxtItemCategory.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtItemCategory.AgSelectedValue = Nothing
        Me.TxtItemCategory.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtItemCategory.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtItemCategory.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtItemCategory.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtItemCategory.Location = New System.Drawing.Point(131, 156)
        Me.TxtItemCategory.MaxLength = 20
        Me.TxtItemCategory.Name = "TxtItemCategory"
        Me.TxtItemCategory.Size = New System.Drawing.Size(131, 18)
        Me.TxtItemCategory.TabIndex = 10
        '
        'TxtItemGroup
        '
        Me.TxtItemGroup.AgAllowUserToEnableMasterHelp = False
        Me.TxtItemGroup.AgLastValueTag = Nothing
        Me.TxtItemGroup.AgLastValueText = Nothing
        Me.TxtItemGroup.AgMandatory = True
        Me.TxtItemGroup.AgMasterHelp = False
        Me.TxtItemGroup.AgNumberLeftPlaces = 0
        Me.TxtItemGroup.AgNumberNegetiveAllow = False
        Me.TxtItemGroup.AgNumberRightPlaces = 0
        Me.TxtItemGroup.AgPickFromLastValue = False
        Me.TxtItemGroup.AgRowFilter = ""
        Me.TxtItemGroup.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtItemGroup.AgSelectedValue = Nothing
        Me.TxtItemGroup.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtItemGroup.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtItemGroup.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtItemGroup.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtItemGroup.Location = New System.Drawing.Point(131, 136)
        Me.TxtItemGroup.MaxLength = 20
        Me.TxtItemGroup.Name = "TxtItemGroup"
        Me.TxtItemGroup.Size = New System.Drawing.Size(131, 18)
        Me.TxtItemGroup.TabIndex = 8
        '
        'LblItemGroup
        '
        Me.LblItemGroup.AutoSize = True
        Me.LblItemGroup.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblItemGroup.Location = New System.Drawing.Point(9, 136)
        Me.LblItemGroup.Name = "LblItemGroup"
        Me.LblItemGroup.Size = New System.Drawing.Size(72, 16)
        Me.LblItemGroup.TabIndex = 1042
        Me.LblItemGroup.Text = "Item Group"
        '
        'TxtSalesTaxPostingGroup
        '
        Me.TxtSalesTaxPostingGroup.AgAllowUserToEnableMasterHelp = False
        Me.TxtSalesTaxPostingGroup.AgLastValueTag = Nothing
        Me.TxtSalesTaxPostingGroup.AgLastValueText = Nothing
        Me.TxtSalesTaxPostingGroup.AgMandatory = True
        Me.TxtSalesTaxPostingGroup.AgMasterHelp = False
        Me.TxtSalesTaxPostingGroup.AgNumberLeftPlaces = 0
        Me.TxtSalesTaxPostingGroup.AgNumberNegetiveAllow = False
        Me.TxtSalesTaxPostingGroup.AgNumberRightPlaces = 0
        Me.TxtSalesTaxPostingGroup.AgPickFromLastValue = False
        Me.TxtSalesTaxPostingGroup.AgRowFilter = ""
        Me.TxtSalesTaxPostingGroup.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtSalesTaxPostingGroup.AgSelectedValue = Nothing
        Me.TxtSalesTaxPostingGroup.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtSalesTaxPostingGroup.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtSalesTaxPostingGroup.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtSalesTaxPostingGroup.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSalesTaxPostingGroup.Location = New System.Drawing.Point(131, 176)
        Me.TxtSalesTaxPostingGroup.MaxLength = 20
        Me.TxtSalesTaxPostingGroup.Name = "TxtSalesTaxPostingGroup"
        Me.TxtSalesTaxPostingGroup.Size = New System.Drawing.Size(131, 18)
        Me.TxtSalesTaxPostingGroup.TabIndex = 12
        '
        'LblSalesTaxPostingGroup
        '
        Me.LblSalesTaxPostingGroup.AutoSize = True
        Me.LblSalesTaxPostingGroup.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblSalesTaxPostingGroup.Location = New System.Drawing.Point(9, 176)
        Me.LblSalesTaxPostingGroup.Name = "LblSalesTaxPostingGroup"
        Me.LblSalesTaxPostingGroup.Size = New System.Drawing.Size(104, 16)
        Me.LblSalesTaxPostingGroup.TabIndex = 1041
        Me.LblSalesTaxPostingGroup.Text = "Sales Tax Group"
        '
        'LblManualCodeReq
        '
        Me.LblManualCodeReq.AutoSize = True
        Me.LblManualCodeReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblManualCodeReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblManualCodeReq.Location = New System.Drawing.Point(115, 58)
        Me.LblManualCodeReq.Name = "LblManualCodeReq"
        Me.LblManualCodeReq.Size = New System.Drawing.Size(10, 7)
        Me.LblManualCodeReq.TabIndex = 1040
        Me.LblManualCodeReq.Text = "Ä"
        '
        'TxtManualCode
        '
        Me.TxtManualCode.AgAllowUserToEnableMasterHelp = False
        Me.TxtManualCode.AgLastValueTag = Nothing
        Me.TxtManualCode.AgLastValueText = ""
        Me.TxtManualCode.AgMandatory = True
        Me.TxtManualCode.AgMasterHelp = True
        Me.TxtManualCode.AgNumberLeftPlaces = 0
        Me.TxtManualCode.AgNumberNegetiveAllow = False
        Me.TxtManualCode.AgNumberRightPlaces = 0
        Me.TxtManualCode.AgPickFromLastValue = False
        Me.TxtManualCode.AgRowFilter = ""
        Me.TxtManualCode.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtManualCode.AgSelectedValue = Nothing
        Me.TxtManualCode.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtManualCode.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtManualCode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtManualCode.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtManualCode.Location = New System.Drawing.Point(131, 56)
        Me.TxtManualCode.MaxLength = 20
        Me.TxtManualCode.Name = "TxtManualCode"
        Me.TxtManualCode.Size = New System.Drawing.Size(273, 18)
        Me.TxtManualCode.TabIndex = 1
        '
        'LblManualCode
        '
        Me.LblManualCode.AutoSize = True
        Me.LblManualCode.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblManualCode.Location = New System.Drawing.Point(9, 56)
        Me.LblManualCode.Name = "LblManualCode"
        Me.LblManualCode.Size = New System.Drawing.Size(67, 16)
        Me.LblManualCode.TabIndex = 1039
        Me.LblManualCode.Text = "Item Code"
        '
        'TxtUnit
        '
        Me.TxtUnit.AgAllowUserToEnableMasterHelp = False
        Me.TxtUnit.AgLastValueTag = Nothing
        Me.TxtUnit.AgLastValueText = Nothing
        Me.TxtUnit.AgMandatory = True
        Me.TxtUnit.AgMasterHelp = False
        Me.TxtUnit.AgNumberLeftPlaces = 0
        Me.TxtUnit.AgNumberNegetiveAllow = False
        Me.TxtUnit.AgNumberRightPlaces = 0
        Me.TxtUnit.AgPickFromLastValue = False
        Me.TxtUnit.AgRowFilter = ""
        Me.TxtUnit.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtUnit.AgSelectedValue = Nothing
        Me.TxtUnit.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtUnit.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtUnit.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtUnit.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtUnit.Location = New System.Drawing.Point(131, 96)
        Me.TxtUnit.MaxLength = 20
        Me.TxtUnit.Name = "TxtUnit"
        Me.TxtUnit.Size = New System.Drawing.Size(131, 18)
        Me.TxtUnit.TabIndex = 4
        '
        'LblUnit
        '
        Me.LblUnit.AutoSize = True
        Me.LblUnit.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblUnit.Location = New System.Drawing.Point(9, 96)
        Me.LblUnit.Name = "LblUnit"
        Me.LblUnit.Size = New System.Drawing.Size(31, 16)
        Me.LblUnit.TabIndex = 1038
        Me.LblUnit.Text = "Unit"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(115, 78)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(10, 7)
        Me.Label1.TabIndex = 1037
        Me.Label1.Text = "Ä"
        '
        'TxtDescription
        '
        Me.TxtDescription.AgAllowUserToEnableMasterHelp = False
        Me.TxtDescription.AgLastValueTag = Nothing
        Me.TxtDescription.AgLastValueText = Nothing
        Me.TxtDescription.AgMandatory = True
        Me.TxtDescription.AgMasterHelp = True
        Me.TxtDescription.AgNumberLeftPlaces = 0
        Me.TxtDescription.AgNumberNegetiveAllow = False
        Me.TxtDescription.AgNumberRightPlaces = 0
        Me.TxtDescription.AgPickFromLastValue = False
        Me.TxtDescription.AgRowFilter = ""
        Me.TxtDescription.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtDescription.AgSelectedValue = Nothing
        Me.TxtDescription.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtDescription.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtDescription.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtDescription.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDescription.Location = New System.Drawing.Point(131, 76)
        Me.TxtDescription.MaxLength = 255
        Me.TxtDescription.Name = "TxtDescription"
        Me.TxtDescription.Size = New System.Drawing.Size(414, 18)
        Me.TxtDescription.TabIndex = 3
        '
        'LblDescription
        '
        Me.LblDescription.AutoSize = True
        Me.LblDescription.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDescription.Location = New System.Drawing.Point(9, 76)
        Me.LblDescription.Name = "LblDescription"
        Me.LblDescription.Size = New System.Drawing.Size(71, 16)
        Me.LblDescription.TabIndex = 1036
        Me.LblDescription.Text = "Item Name"
        '
        'GBOtherDetails
        '
        Me.GBOtherDetails.BackColor = System.Drawing.Color.Transparent
        Me.GBOtherDetails.Controls.Add(Me.Label13)
        Me.GBOtherDetails.Controls.Add(Me.TxtStdWeight)
        Me.GBOtherDetails.Controls.Add(Me.TxtIsRestrictedinTransaction)
        Me.GBOtherDetails.Controls.Add(Me.Label7)
        Me.GBOtherDetails.Controls.Add(Me.TxtIsUnitConversionMandatory)
        Me.GBOtherDetails.Controls.Add(Me.Label11)
        Me.GBOtherDetails.Controls.Add(Me.TxtPurchQtyAllowedWithoutPO)
        Me.GBOtherDetails.Controls.Add(Me.TxtMaximumStockLevel)
        Me.GBOtherDetails.Controls.Add(Me.Label21)
        Me.GBOtherDetails.Controls.Add(Me.Label16)
        Me.GBOtherDetails.Controls.Add(Me.TxtIsRequired_LotNo)
        Me.GBOtherDetails.Controls.Add(Me.TxtMinimumStockLevel)
        Me.GBOtherDetails.Controls.Add(Me.Label20)
        Me.GBOtherDetails.Controls.Add(Me.Label19)
        Me.GBOtherDetails.Controls.Add(Me.TxtBinLocation)
        Me.GBOtherDetails.Controls.Add(Me.Label17)
        Me.GBOtherDetails.Controls.Add(Me.Label18)
        Me.GBOtherDetails.Controls.Add(Me.TxtReOrderStockLevel)
        Me.GBOtherDetails.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBOtherDetails.Location = New System.Drawing.Point(9, 262)
        Me.GBOtherDetails.Name = "GBOtherDetails"
        Me.GBOtherDetails.Size = New System.Drawing.Size(536, 159)
        Me.GBOtherDetails.TabIndex = 16
        Me.GBOtherDetails.TabStop = False
        Me.GBOtherDetails.Text = "Other Details"
        '
        'TxtIsRestrictedinTransaction
        '
        Me.TxtIsRestrictedinTransaction.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsRestrictedinTransaction.AgLastValueTag = Nothing
        Me.TxtIsRestrictedinTransaction.AgLastValueText = Nothing
        Me.TxtIsRestrictedinTransaction.AgMandatory = False
        Me.TxtIsRestrictedinTransaction.AgMasterHelp = False
        Me.TxtIsRestrictedinTransaction.AgNumberLeftPlaces = 0
        Me.TxtIsRestrictedinTransaction.AgNumberNegetiveAllow = False
        Me.TxtIsRestrictedinTransaction.AgNumberRightPlaces = 0
        Me.TxtIsRestrictedinTransaction.AgPickFromLastValue = False
        Me.TxtIsRestrictedinTransaction.AgRowFilter = ""
        Me.TxtIsRestrictedinTransaction.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsRestrictedinTransaction.AgSelectedValue = Nothing
        Me.TxtIsRestrictedinTransaction.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsRestrictedinTransaction.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsRestrictedinTransaction.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsRestrictedinTransaction.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsRestrictedinTransaction.Location = New System.Drawing.Point(174, 27)
        Me.TxtIsRestrictedinTransaction.MaxLength = 20
        Me.TxtIsRestrictedinTransaction.Name = "TxtIsRestrictedinTransaction"
        Me.TxtIsRestrictedinTransaction.Size = New System.Drawing.Size(79, 18)
        Me.TxtIsRestrictedinTransaction.TabIndex = 0
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(9, 29)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(166, 16)
        Me.Label7.TabIndex = 756
        Me.Label7.Text = "Is Restricted In Transaction"
        '
        'TxtIsUnitConversionMandatory
        '
        Me.TxtIsUnitConversionMandatory.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsUnitConversionMandatory.AgLastValueTag = Nothing
        Me.TxtIsUnitConversionMandatory.AgLastValueText = Nothing
        Me.TxtIsUnitConversionMandatory.AgMandatory = False
        Me.TxtIsUnitConversionMandatory.AgMasterHelp = False
        Me.TxtIsUnitConversionMandatory.AgNumberLeftPlaces = 0
        Me.TxtIsUnitConversionMandatory.AgNumberNegetiveAllow = False
        Me.TxtIsUnitConversionMandatory.AgNumberRightPlaces = 0
        Me.TxtIsUnitConversionMandatory.AgPickFromLastValue = False
        Me.TxtIsUnitConversionMandatory.AgRowFilter = ""
        Me.TxtIsUnitConversionMandatory.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsUnitConversionMandatory.AgSelectedValue = Nothing
        Me.TxtIsUnitConversionMandatory.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsUnitConversionMandatory.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsUnitConversionMandatory.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsUnitConversionMandatory.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsUnitConversionMandatory.Location = New System.Drawing.Point(450, 25)
        Me.TxtIsUnitConversionMandatory.MaxLength = 20
        Me.TxtIsUnitConversionMandatory.Name = "TxtIsUnitConversionMandatory"
        Me.TxtIsUnitConversionMandatory.Size = New System.Drawing.Size(80, 18)
        Me.TxtIsUnitConversionMandatory.TabIndex = 1
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(256, 26)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(188, 16)
        Me.Label11.TabIndex = 754
        Me.Label11.Text = "Is Unit Conversion Mandatory ?"
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.LblMaterialPlanForFollowingItems)
        Me.Panel1.Controls.Add(Me.PicPhoto)
        Me.Panel1.Controls.Add(Me.BtnBrowse)
        Me.Panel1.Controls.Add(Me.BtnPhotoClear)
        Me.Panel1.Location = New System.Drawing.Point(572, 248)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(169, 192)
        Me.Panel1.TabIndex = 18
        '
        'LblMaterialPlanForFollowingItems
        '
        Me.LblMaterialPlanForFollowingItems.BackColor = System.Drawing.Color.SteelBlue
        Me.LblMaterialPlanForFollowingItems.DisabledLinkColor = System.Drawing.Color.White
        Me.LblMaterialPlanForFollowingItems.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMaterialPlanForFollowingItems.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LblMaterialPlanForFollowingItems.LinkColor = System.Drawing.Color.White
        Me.LblMaterialPlanForFollowingItems.Location = New System.Drawing.Point(-1, 0)
        Me.LblMaterialPlanForFollowingItems.Name = "LblMaterialPlanForFollowingItems"
        Me.LblMaterialPlanForFollowingItems.Size = New System.Drawing.Size(169, 25)
        Me.LblMaterialPlanForFollowingItems.TabIndex = 19
        Me.LblMaterialPlanForFollowingItems.TabStop = True
        Me.LblMaterialPlanForFollowingItems.Text = "Item Image"
        Me.LblMaterialPlanForFollowingItems.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BtnUnitConversion
        '
        Me.BtnUnitConversion.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnUnitConversion.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnUnitConversion.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnUnitConversion.Location = New System.Drawing.Point(783, 251)
        Me.BtnUnitConversion.Name = "BtnUnitConversion"
        Me.BtnUnitConversion.Size = New System.Drawing.Size(131, 23)
        Me.BtnUnitConversion.TabIndex = 19
        Me.BtnUnitConversion.Text = "Unit Conversion"
        Me.BtnUnitConversion.UseVisualStyleBackColor = True
        '
        'BtnBOMDetail
        '
        Me.BtnBOMDetail.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnBOMDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnBOMDetail.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnBOMDetail.Location = New System.Drawing.Point(783, 280)
        Me.BtnBOMDetail.Name = "BtnBOMDetail"
        Me.BtnBOMDetail.Size = New System.Drawing.Size(131, 23)
        Me.BtnBOMDetail.TabIndex = 20
        Me.BtnBOMDetail.Text = "BOM Detail"
        Me.BtnBOMDetail.UseVisualStyleBackColor = True
        '
        'ChkIsSystemDefine
        '
        Me.ChkIsSystemDefine.AutoSize = True
        Me.ChkIsSystemDefine.BackColor = System.Drawing.Color.Transparent
        Me.ChkIsSystemDefine.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkIsSystemDefine.ForeColor = System.Drawing.Color.Red
        Me.ChkIsSystemDefine.Location = New System.Drawing.Point(410, 58)
        Me.ChkIsSystemDefine.Name = "ChkIsSystemDefine"
        Me.ChkIsSystemDefine.Size = New System.Drawing.Size(15, 14)
        Me.ChkIsSystemDefine.TabIndex = 1058
        Me.ChkIsSystemDefine.UseVisualStyleBackColor = False
        '
        'LblIsSystemDefine
        '
        Me.LblIsSystemDefine.AutoSize = True
        Me.LblIsSystemDefine.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblIsSystemDefine.ForeColor = System.Drawing.Color.Red
        Me.LblIsSystemDefine.Location = New System.Drawing.Point(424, 57)
        Me.LblIsSystemDefine.Name = "LblIsSystemDefine"
        Me.LblIsSystemDefine.Size = New System.Drawing.Size(96, 15)
        Me.LblIsSystemDefine.TabIndex = 1059
        Me.LblIsSystemDefine.Text = "IsSystemDefine"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label12.Location = New System.Drawing.Point(115, 159)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(10, 7)
        Me.Label12.TabIndex = 1060
        Me.Label12.Text = "Ä"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(9, 107)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(116, 16)
        Me.Label13.TabIndex = 758
        Me.Label13.Text = "Std. Weight ( KG )"
        '
        'TxtStdWeight
        '
        Me.TxtStdWeight.AgAllowUserToEnableMasterHelp = False
        Me.TxtStdWeight.AgLastValueTag = Nothing
        Me.TxtStdWeight.AgLastValueText = Nothing
        Me.TxtStdWeight.AgMandatory = False
        Me.TxtStdWeight.AgMasterHelp = False
        Me.TxtStdWeight.AgNumberLeftPlaces = 6
        Me.TxtStdWeight.AgNumberNegetiveAllow = False
        Me.TxtStdWeight.AgNumberRightPlaces = 3
        Me.TxtStdWeight.AgPickFromLastValue = False
        Me.TxtStdWeight.AgRowFilter = ""
        Me.TxtStdWeight.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtStdWeight.AgSelectedValue = Nothing
        Me.TxtStdWeight.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtStdWeight.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtStdWeight.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtStdWeight.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtStdWeight.Location = New System.Drawing.Point(174, 107)
        Me.TxtStdWeight.MaxLength = 10
        Me.TxtStdWeight.Name = "TxtStdWeight"
        Me.TxtStdWeight.Size = New System.Drawing.Size(79, 18)
        Me.TxtStdWeight.TabIndex = 757
        '
        'FrmItemMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(944, 492)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.LblIsSystemDefine)
        Me.Controls.Add(Me.ChkIsSystemDefine)
        Me.Controls.Add(Me.BtnBOMDetail)
        Me.Controls.Add(Me.BtnUnitConversion)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.GBOtherDetails)
        Me.Controls.Add(Me.TxtTariffHead)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtVatCommodity)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.PnlCustomGrid)
        Me.Controls.Add(Me.TxtItemInvoiceGroup)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.TxtSpecification)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TxtMeasurePerPcs)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TxtMeasureUnit)
        Me.Controls.Add(Me.LblMeasureUnit)
        Me.Controls.Add(Me.LblItemCategory)
        Me.Controls.Add(Me.TxtItemGroup)
        Me.Controls.Add(Me.LblItemGroup)
        Me.Controls.Add(Me.TxtSalesTaxPostingGroup)
        Me.Controls.Add(Me.LblSalesTaxPostingGroup)
        Me.Controls.Add(Me.LblManualCodeReq)
        Me.Controls.Add(Me.TxtManualCode)
        Me.Controls.Add(Me.TxtItemType)
        Me.Controls.Add(Me.TxtRate)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.LblManualCode)
        Me.Controls.Add(Me.TxtUnit)
        Me.Controls.Add(Me.LblUnit)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtDescription)
        Me.Controls.Add(Me.LblDescription)
        Me.Controls.Add(Me.TxtCustomFields)
        Me.Controls.Add(Me.TxtItemCategory)
        Me.Name = "FrmItemMaster"
        Me.Text = "Item Master"
        Me.Controls.SetChildIndex(Me.TxtItemCategory, 0)
        Me.Controls.SetChildIndex(Me.TxtCustomFields, 0)
        Me.Controls.SetChildIndex(Me.LblDescription, 0)
        Me.Controls.SetChildIndex(Me.TxtDescription, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.LblUnit, 0)
        Me.Controls.SetChildIndex(Me.TxtUnit, 0)
        Me.Controls.SetChildIndex(Me.LblManualCode, 0)
        Me.Controls.SetChildIndex(Me.Label3, 0)
        Me.Controls.SetChildIndex(Me.TxtRate, 0)
        Me.Controls.SetChildIndex(Me.TxtItemType, 0)
        Me.Controls.SetChildIndex(Me.TxtManualCode, 0)
        Me.Controls.SetChildIndex(Me.LblManualCodeReq, 0)
        Me.Controls.SetChildIndex(Me.LblSalesTaxPostingGroup, 0)
        Me.Controls.SetChildIndex(Me.TxtSalesTaxPostingGroup, 0)
        Me.Controls.SetChildIndex(Me.LblItemGroup, 0)
        Me.Controls.SetChildIndex(Me.TxtItemGroup, 0)
        Me.Controls.SetChildIndex(Me.LblItemCategory, 0)
        Me.Controls.SetChildIndex(Me.LblMeasureUnit, 0)
        Me.Controls.SetChildIndex(Me.TxtMeasureUnit, 0)
        Me.Controls.SetChildIndex(Me.Label4, 0)
        Me.Controls.SetChildIndex(Me.TxtMeasurePerPcs, 0)
        Me.Controls.SetChildIndex(Me.Label5, 0)
        Me.Controls.SetChildIndex(Me.Label8, 0)
        Me.Controls.SetChildIndex(Me.Label6, 0)
        Me.Controls.SetChildIndex(Me.Label10, 0)
        Me.Controls.SetChildIndex(Me.Label15, 0)
        Me.Controls.SetChildIndex(Me.TxtSpecification, 0)
        Me.Controls.SetChildIndex(Me.Label9, 0)
        Me.Controls.SetChildIndex(Me.TxtItemInvoiceGroup, 0)
        Me.Controls.SetChildIndex(Me.PnlCustomGrid, 0)
        Me.Controls.SetChildIndex(Me.Label14, 0)
        Me.Controls.SetChildIndex(Me.TxtVatCommodity, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.TxtTariffHead, 0)
        Me.Controls.SetChildIndex(Me.GBOtherDetails, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.BtnUnitConversion, 0)
        Me.Controls.SetChildIndex(Me.BtnBOMDetail, 0)
        Me.Controls.SetChildIndex(Me.ChkIsSystemDefine, 0)
        Me.Controls.SetChildIndex(Me.LblIsSystemDefine, 0)
        Me.Controls.SetChildIndex(Me.Label12, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.GrpUP.ResumeLayout(False)
        Me.GrpUP.PerformLayout()
        Me.GBoxEntryType.ResumeLayout(False)
        Me.GBoxEntryType.PerformLayout()
        Me.GBoxMoveToLog.ResumeLayout(False)
        Me.GBoxMoveToLog.PerformLayout()
        Me.GBoxApprove.ResumeLayout(False)
        Me.GBoxApprove.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GBoxDivision.ResumeLayout(False)
        Me.GBoxDivision.PerformLayout()
        CType(Me.DTMaster, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicPhoto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GBOtherDetails.ResumeLayout(False)
        Me.GBOtherDetails.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents TxtCustomFields As AgControls.AgTextBox
    Public WithEvents PicPhoto As System.Windows.Forms.PictureBox
    Public WithEvents BtnBrowse As System.Windows.Forms.Button
    Public WithEvents BtnPhotoClear As System.Windows.Forms.Button
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents TxtTariffHead As AgControls.AgTextBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents LblItemCategory As System.Windows.Forms.Label
    Public WithEvents TxtVatCommodity As AgControls.AgTextBox
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents PnlCustomGrid As System.Windows.Forms.Panel
    Public WithEvents TxtItemInvoiceGroup As AgControls.AgTextBox
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents TxtSpecification As AgControls.AgTextBox
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents TxtMeasurePerPcs As AgControls.AgTextBox
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents TxtMeasureUnit As AgControls.AgTextBox
    Public WithEvents LblMeasureUnit As System.Windows.Forms.Label
    Public WithEvents TxtItemType As AgControls.AgTextBox
    Public WithEvents TxtRate As AgControls.AgTextBox
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents TxtItemCategory As AgControls.AgTextBox
    Public WithEvents TxtItemGroup As AgControls.AgTextBox
    Public WithEvents LblItemGroup As System.Windows.Forms.Label
    Public WithEvents TxtSalesTaxPostingGroup As AgControls.AgTextBox
    Public WithEvents LblSalesTaxPostingGroup As System.Windows.Forms.Label
    Public WithEvents LblManualCodeReq As System.Windows.Forms.Label
    Public WithEvents TxtManualCode As AgControls.AgTextBox
    Public WithEvents LblManualCode As System.Windows.Forms.Label
    Public WithEvents TxtUnit As AgControls.AgTextBox
    Public WithEvents LblUnit As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents TxtDescription As AgControls.AgTextBox
    Public WithEvents LblDescription As System.Windows.Forms.Label
    Public WithEvents TxtPurchQtyAllowedWithoutPO As AgControls.AgTextBox
    Public WithEvents Label21 As System.Windows.Forms.Label
    Public WithEvents TxtIsRequired_LotNo As AgControls.AgTextBox
    Public WithEvents Label20 As System.Windows.Forms.Label
    Public WithEvents TxtBinLocation As AgControls.AgTextBox
    Public WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents TxtMaximumStockLevel As AgControls.AgTextBox
    Public WithEvents TxtReOrderStockLevel As AgControls.AgTextBox
    Public WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents Label19 As System.Windows.Forms.Label
    Public WithEvents TxtMinimumStockLevel As AgControls.AgTextBox
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents GBOtherDetails As System.Windows.Forms.GroupBox
    Public WithEvents Panel1 As System.Windows.Forms.Panel
    Public WithEvents LblMaterialPlanForFollowingItems As System.Windows.Forms.LinkLabel
    Public WithEvents AgCustomGrid1 As New AgCustomFields.AgCustomGrid
    Public WithEvents BtnUnitConversion As System.Windows.Forms.Button
    Public WithEvents BtnBOMDetail As System.Windows.Forms.Button
    Public WithEvents TxtIsUnitConversionMandatory As AgControls.AgTextBox
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents TxtIsRestrictedinTransaction As AgControls.AgTextBox
    Public WithEvents Label7 As System.Windows.Forms.Label
#End Region

    Private Sub FrmItemMasterNew_BaseEvent_ApproveDeletion_InTrans(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand) Handles Me.BaseEvent_ApproveDeletion_InTrans
        mQry = "DELETE FROM UnitConversion WHERE Item = '" & mSearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = "DELETE FROM BOMDetail WHERE BaseItem = '" & mSearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    End Sub

    Private Sub FrmYarn_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        If AgL.RequiredField(TxtManualCode, LblManualCode.Text) Then passed = False : Exit Sub
        If AgL.RequiredField(TxtDescription, LblDescription.Text) Then passed = False : Exit Sub
        If AgL.RequiredField(TxtUnit, LblUnit.Text) Then passed = False : Exit Sub
        If AgL.RequiredField(TxtItemGroup, LblItemGroup.Text) Then passed = False : Exit Sub
        If AgL.RequiredField(TxtItemCategory, LblItemCategory.Text) Then passed = False : Exit Sub
        If AgL.RequiredField(TxtSalesTaxPostingGroup, LblSalesTaxPostingGroup.Text) Then passed = False : Exit Sub

        If Topctrl1.Mode = "Add" Then
            mQry = "Select count(*) From Item Where ManualCode ='" & TxtManualCode.Text & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Short Name Already Exist!")

            mQry = "Select count(*) From Item Where Description='" & TxtDescription.Text & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")
        Else
            mQry = "Select count(*) From Item Where ManualCode ='" & TxtManualCode.Text & "' And Code < >'" & mInternalCode & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Short Name Already Exist!")

            mQry = "Select count(*) From Item Where Description='" & TxtDescription.Text & "' And Code <> '" & mInternalCode & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")
        End If
    End Sub

    Private Sub FrmYarn_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mConStr$ = " Where H.ItemType In ('" & AgTemplate.ClsMain.ItemType.FinishedMaterial & "','" & AgTemplate.ClsMain.ItemType.RawMaterial & "','" & AgTemplate.ClsMain.ItemType.Other & "','" & AgTemplate.ClsMain.ItemType.SemiFinishedMaterial & "') "
        mQry = "Select H.Code As SearchCode " & _
                " From Item H " & mConStr & _
                " Order By H.Description "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Public Sub FrmYarn_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mConStr$ = " Where I.ItemType In ('" & AgTemplate.ClsMain.ItemType.FinishedMaterial & "','" & AgTemplate.ClsMain.ItemType.RawMaterial & "','" & AgTemplate.ClsMain.ItemType.Other & "','" & AgTemplate.ClsMain.ItemType.SemiFinishedMaterial & "')  "
        AgL.PubFindQry = "SELECT I.Code, I.ManualCode as [Item Code], I.Description [Item Description],I.Specification, " & _
                        " IG.Description AS [Item Group], IC.Description AS [Item Category], IT.Name AS [Item Type], IIG.Description AS [Item Invoice Group],I.Unit " & _
                        " FROM Item I " & _
                        " LEFT JOIN ItemGroup IG ON IG.Code = I.ItemGroup " & _
                        " LEFT JOIN ItemCategory IC ON IC.Code = I.ItemCategory " & _
                        " LEFT JOIN ItemType IT ON IT.Code = I.ItemType " & _
                        " LEFT JOIN ItemInvoiceGroup IIG ON IIG.Code = I.ItemInvoiceGroup " & mConStr
        AgL.PubFindQryOrdBy = "[Item Description]"
    End Sub

    Private Sub FrmYarn_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "Item"
        LogTableName = "Item_LOG"
        MainLineTableCsv = "ItemSiteDetail"
        LogLineTableCsv = "ItemSiteDetail_Log"

        PrimaryField = "Code"

        AgL.AddAgDataGrid(AgCustomGrid1, PnlCustomGrid)

        AgCustomGrid1.AgLibVar = AgL
        AgCustomGrid1.SplitGrid = True
        AgCustomGrid1.MnuText = Me.Name
    End Sub

    Private Sub FrmYarn_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand) Handles Me.BaseEvent_Save_InTrans
        mQry = "UPDATE Item " & _
                " SET " & _
                " ManualCode = " & AgL.Chk_Text(TxtManualCode.Text) & ", " & _
                " Description = " & AgL.Chk_Text(TxtDescription.Text) & ", " & _
                " Unit = " & AgL.Chk_Text(TxtUnit.Text) & ", " & _
                " Measure = " & Val(TxtMeasurePerPcs.Text) & ", " & _
                " MeasureUnit = " & AgL.Chk_Text(TxtMeasureUnit.Text) & ", " & _
                " Rate = " & Val(TxtRate.Text) & ", " & _
                " Gross_Weight = " & Val(TxtStdWeight.Text) & ", " & _
                " ItemGroup = " & AgL.Chk_Text(TxtItemGroup.AgSelectedValue) & ", " & _
                " ItemInvoiceGroup = " & AgL.Chk_Text(TxtItemInvoiceGroup.Tag) & ", " & _
                " ItemCategory = " & AgL.Chk_Text(TxtItemCategory.Tag) & ", " & _
                " ItemType = " & AgL.Chk_Text(TxtItemType.Tag) & ", " & _
                " Specification = " & AgL.Chk_Text(TxtSpecification.Text) & ", " & _
                " IsRestricted_InTransaction = " & IIf(TxtIsRestrictedinTransaction.Text = "Yes", 1, 0) & ", " & _
                " VatCommodityCode = " & AgL.Chk_Text(TxtVatCommodity.Tag) & ", " & _
                " TariffHead = " & AgL.Chk_Text(TxtTariffHead.Tag) & ", " & _
                " ServiceTaxYN = 'N', " & _
                " StockYN = 1, " & _
                " IsSystemDefine = " & Val(IIf(ChkIsSystemDefine.Checked, 1, 0)) & ", " & _
                " SalesTaxPostingGroup = " & AgL.Chk_Text(TxtSalesTaxPostingGroup.Text) & ", " & _
                " CustomFields = " & AgL.Chk_Text(TxtCustomFields.Tag) & " " & _
                " " & AgCustomGrid1.FFooterTableUpdateStr() & " " & _
                " Where Code = '" & SearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        Call FPostRateInRateList(Conn, Cmd)
        Call FUpdateOtherItemDetail(Conn, Cmd)

        If BtnUnitConversion.Tag IsNot Nothing Then
            Call FSaveUnitConversion(Conn, Cmd)
        End If

        If BtnBOMDetail.Tag IsNot Nothing Then
            Call FSaveBOMDetail(Conn, Cmd)
        End If

        mQry = "Delete From Item_Image Where Code = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = "Insert Into Item_Image(Code, Photo) Values('" & mSearchCode & "', Null)"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        TxtManualCode.AgLastValueText = TxtManualCode.Text
        TxtUnit.AgLastValueTag = TxtUnit.Tag
        TxtUnit.AgLastValueText = TxtUnit.Text
        TxtMeasureUnit.AgLastValueTag = TxtMeasureUnit.Tag
        TxtMeasureUnit.AgLastValueText = TxtMeasureUnit.Text
        TxtMeasurePerPcs.AgLastValueText = TxtMeasurePerPcs.Text
        TxtRate.AgLastValueText = TxtRate.Text
        TxtItemGroup.AgLastValueTag = TxtItemGroup.Tag
        TxtItemGroup.AgLastValueText = TxtItemGroup.Text
        TxtItemType.AgLastValueTag = TxtItemType.Tag
        TxtItemType.AgLastValueText = TxtItemType.Text
        TxtItemCategory.AgLastValueTag = TxtItemCategory.Tag
        TxtItemCategory.AgLastValueText = TxtItemCategory.Text
        TxtVatCommodity.AgLastValueTag = TxtVatCommodity.Tag
        TxtVatCommodity.AgLastValueText = TxtVatCommodity.Text
        TxtSalesTaxPostingGroup.AgLastValueTag = TxtSalesTaxPostingGroup.Tag
        TxtSalesTaxPostingGroup.AgLastValueText = TxtSalesTaxPostingGroup.Text
        TxtTariffHead.AgLastValueTag = TxtTariffHead.Tag
        TxtTariffHead.AgLastValueText = TxtTariffHead.Text
        TxtItemInvoiceGroup.AgLastValueTag = TxtItemInvoiceGroup.Tag
        TxtItemInvoiceGroup.AgLastValueText = TxtItemInvoiceGroup.Text
        TxtSpecification.AgLastValueText = TxtSpecification.Text

        TxtMinimumStockLevel.AgLastValueText = TxtMinimumStockLevel.Text
        TxtMaximumStockLevel.AgLastValueText = TxtMaximumStockLevel.Text
        TxtReOrderStockLevel.AgLastValueText = TxtReOrderStockLevel.Text
        TxtBinLocation.AgLastValueText = TxtBinLocation.Text
        TxtIsRequired_LotNo.AgLastValueText = TxtIsRequired_LotNo.Text
        TxtPurchQtyAllowedWithoutPO.AgLastValueText = TxtPurchQtyAllowedWithoutPO.Text
        TxtIsUnitConversionMandatory.AgLastValueText = TxtIsUnitConversionMandatory.Text
        TxtIsRestrictedinTransaction.AgLastValueText = TxtIsRestrictedinTransaction.Text

    End Sub

    Private Sub FUpdateOtherItemDetail(ByVal Conn As SqlClient.SqlConnection, ByVal Cmd As SqlClient.SqlCommand)
        mQry = "Select count(*) From ItemSiteDetail With (NoLock) Where Code='" & mSearchCode & "' And Div_Code = '" & AgL.PubDivCode & "' And Site_Code ='" & AgL.PubSiteCode & "' "
        If AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar > 0 Then
            mQry = " UPDATE ItemSiteDetail " & _
                    " SET MinimumStockLevel = " & Val(TxtMinimumStockLevel.Text) & ", " & _
                    " MaximumStockLevel = " & Val(TxtMaximumStockLevel.Text) & ", " & _
                    " ReOrderStockLevel = " & Val(TxtReOrderStockLevel.Text) & ", " & _
                    " IsRequired_LotNo = " & IIf(TxtIsRequired_LotNo.Text = "Yes", 1, 0) & ", " & _
                    " IsMandatory_UnitConversion = " & IIf(TxtIsUnitConversionMandatory.Text = "Yes", 1, 0) & ", " & _
                    " PurchQtyAllowedWithoutPO = " & Val(TxtPurchQtyAllowedWithoutPO.Text) & ", " & _
                    " BinLocation = " & AgL.Chk_Text(TxtBinLocation.Text) & " " & _
                    " WHERE Code = '" & mSearchCode & "' " & _
                    " AND Div_Code = '" & AgL.PubDivCode & "' " & _
                    " AND Site_Code = '" & AgL.PubSiteCode & "' "
        Else
            mQry = " INSERT INTO ItemSiteDetail(Code,Div_Code,Site_Code,MinimumStockLevel,MaximumStockLevel, " & _
                    " ReOrderStockLevel, BinLocation, IsRequired_LotNo,	IsMandatory_UnitConversion, PurchQtyAllowedWithoutPO ) " & _
                    " VALUES ( '" & mSearchCode & "','" & AgL.PubDivCode & "','" & AgL.PubSiteCode & "', " & _
                    " " & Val(TxtMinimumStockLevel.Text) & ", " & Val(TxtMaximumStockLevel.Text) & ", " & _
                    " " & Val(TxtReOrderStockLevel.Text) & " ," & AgL.Chk_Text(TxtBinLocation.Text) & " ," & _
                    " " & IIf(TxtIsRequired_LotNo.Text = "Yes", 1, 0) & ",	" & IIf(TxtIsUnitConversionMandatory.Text = "Yes", 1, 0) & ", " & Val(TxtPurchQtyAllowedWithoutPO.Text) & " )"
        End If
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    End Sub

    Private Sub FPostRateInRateList(ByVal Conn As SqlClient.SqlConnection, ByVal Cmd As SqlClient.SqlCommand)
        Dim bRateListCode$ = ""
        bRateListCode = AgL.GetMaxId("RateList", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 8, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)

        mQry = " INSERT INTO RateList(Code, WEF, RateType, EntryBy, EntryDate, EntryType, " & _
                " EntryStatus, Status, Div_Code) " & _
                " VALUES (" & AgL.Chk_Text(bRateListCode) & ", " & AgL.Chk_Text(AgL.PubLoginDate) & ",	" & _
                " NULL,	'" & AgL.PubUserName & "', '" & AgL.PubLoginDate & "', " & _
                " '" & Topctrl1.Mode & "', 'Open', '" & AgTemplate.ClsMain.EntryStatus.Active & "', " & _
                " '" & TxtDivision.AgSelectedValue & "')"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = "INSERT INTO RateListDetail(Code, Sr, WEF, Item, RateType, Rate) " & _
              " VALUES (" & AgL.Chk_Text(bRateListCode) & ", " & _
              " 1, " & AgL.Chk_Text(AgL.PubStartDate) & ", " & _
              " " & AgL.Chk_Text(mSearchCode) & ", " & _
              " NULL, " & Val(TxtRate.Text) & " ) "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    End Sub

    Private Sub FSaveUnitConversion(ByVal Conn As SqlClient.SqlConnection, ByVal Cmd As SqlClient.SqlCommand)
        Dim I As Integer
        mQry = "DELETE FROM UnitConversion WHERE Item = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        If BtnUnitConversion.Tag IsNot Nothing Then
            With BtnUnitConversion.Tag.Dgl1
                For I = 0 To .Rows.Count - 1
                    If .Item(FrmItemMasterUnitConversion.Col1FromUnit, I).Value <> "" Then
                        mQry = " INSERT INTO UnitConversion ( Item,FromUnit,ToUnit,FromQty,ToQty,Multiplier,EntryBy,EntryDate,EntryType,EntryStatus, " & _
                                " Status,Div_Code ) " & _
                                " VALUES ( " & AgL.Chk_Text(mSearchCode) & ", " & _
                                " " & AgL.Chk_Text(.Item(FrmItemMasterUnitConversion.Col1FromUnit, I).Value) & ", " & _
                                " " & AgL.Chk_Text(.Item(FrmItemMasterUnitConversion.Col1ToUnit, I).Value) & ", " & _
                                " " & Val(.Item(FrmItemMasterUnitConversion.Col1FromQty, I).Value) & ", " & _
                                " " & Val(.Item(FrmItemMasterUnitConversion.Col1ToQty, I).Value) & ", " & _
                                " " & Val(.Item(FrmItemMasterUnitConversion.Col1Multiplier, I).Value) & ", " & _
                                " '" & AgL.PubUserName & "'," & AgL.Chk_Text(AgL.PubLoginDate) & ",	'" & Topctrl1.Mode & "', " & _
                                " 'Open',  '" & AgTemplate.ClsMain.EntryStatus.Active & "' , '" & TxtDivision.AgSelectedValue & "' ) "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    End If
                Next
            End With
        End If
    End Sub

    Private Sub FSaveBOMDetail(ByVal Conn As SqlClient.SqlConnection, ByVal Cmd As SqlClient.SqlCommand)
        Dim I As Integer
        mQry = "DELETE FROM BOMDetail WHERE BaseItem = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        If BtnBOMDetail.Tag IsNot Nothing Then
            With BtnBOMDetail.Tag.Dgl1
                For I = 0 To .Rows.Count - 1
                    If .Item(FrmItemMasterBOMDetail.Col1Item, I).Value <> "" Then
                        mQry = " INSERT INTO BomDetail ( Sr, Item, Qty, Process, Dimension1, Dimension2, " & _
                                " Unit,WastagePer, BatchQty, BatchUnit, BaseItem ) " & _
                                " VALUES ( " & I + 1 & "," & _
                                " " & AgL.Chk_Text(.Item(FrmItemMasterBOMDetail.Col1Item, I).tag) & ", " & _
                                " " & Val(.Item(FrmItemMasterBOMDetail.Col1Qty, I).Value) & ", " & _
                                " " & AgL.Chk_Text(.Item(FrmItemMasterBOMDetail.Col1Process, I).tag) & ", " & _
                                " " & AgL.Chk_Text(.Item(FrmItemMasterBOMDetail.Col1Dimension1, I).tag) & ", " & _
                                " " & AgL.Chk_Text(.Item(FrmItemMasterBOMDetail.Col1Dimension2, I).tag) & ", " & _
                                " " & AgL.Chk_Text(.Item(FrmItemMasterBOMDetail.Col1Unit, I).Value) & ", " & _
                                " " & Val(.Item(FrmItemMasterBOMDetail.Col1WastagePer, I).Value) & ", " & _
                                " " & Val(BtnBOMDetail.Tag.TxtBatchQty.Text) & ", " & _
                                " " & AgL.Chk_Text(BtnBOMDetail.Tag.LblUnit.Text) & ", " & _
                                " " & AgL.Chk_Text(mSearchCode) & "	) "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    End If
                Next
            End With
        End If
    End Sub

    Private Sub FrmQuality1_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DsTemp As DataSet
        mQry = "Select I.*, Ig.Description As ItemGroupDesc, IC.Description As ItemCategoryDesc, B.Description As BomDesc, IIG.Description As ItemInvoiceGroupDesc, " & _
                " TH.Description AS TariffHeadDesc, IT.Name AS ItemTypeName, VC.Description AS VatCommodityDesc, isnull(V.Cnt,0) AS Cnt " & _
                " From Item I " & _
                " LEFT JOIN ItemGroup Ig ON I.ItemGroup = IG.Code " & _
                " LEFT JOIN ItemCategory IC ON IC.Code = I.ItemCategory " & _
                " LEFT JOIN ItemType IT ON IT.Code = I.ItemType " & _
                " LEFT JOIN ItemInvoiceGroup IIG ON I.ItemInvoiceGroup = IIG.Code " & _
                " LEFT JOIN Bom B ON I.Bom = B.Code " & _
                " LEFT JOIN TariffHead TH ON TH.Code = I.TariffHead " & _
                " LEFT JOIN VatCommodityCode VC ON VC.Code = I.VatCommodityCode " & _
                " LEFT JOIN ( SELECT L.BaseItem, count(*) AS Cnt  FROM BomDetail L GROUP BY L.BaseItem ) V ON V.BaseItem = I.Code " & _
                " Where I.Code ='" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                mInternalCode = AgL.XNull(.Rows(0)("Code"))
                TxtManualCode.Text = AgL.XNull(.Rows(0)("ManualCode"))
                TxtDescription.Text = AgL.XNull(.Rows(0)("Description"))
                TxtUnit.Text = AgL.XNull(.Rows(0)("Unit"))
                TxtMeasurePerPcs.Text = AgL.VNull(.Rows(0)("Measure"))
                TxtMeasureUnit.Text = AgL.XNull(.Rows(0)("MeasureUnit"))
                TxtRate.Text = AgL.VNull(.Rows(0)("Rate"))
                TxtStdWeight.Text = AgL.VNull(.Rows(0)("Gross_Weight"))
                TxtItemGroup.Tag = AgL.XNull(.Rows(0)("ItemGroup"))
                TxtItemGroup.Text = AgL.XNull(.Rows(0)("ItemGroupDesc"))
                TxtItemCategory.Text = AgL.XNull(.Rows(0)("ItemCategoryDesc"))
                TxtItemCategory.Tag = AgL.XNull(.Rows(0)("ItemCategory"))
                TxtItemType.Text = AgL.XNull(.Rows(0)("ItemTypeName"))
                TxtItemType.Tag = AgL.XNull(.Rows(0)("ItemType"))
                TxtSalesTaxPostingGroup.Text = AgL.XNull(.Rows(0)("SalesTaxPostingGroup"))
                TxtSpecification.Text = AgL.XNull(.Rows(0)("Specification"))
                TxtItemInvoiceGroup.Tag = AgL.XNull(.Rows(0)("ItemInvoiceGroup"))
                TxtItemInvoiceGroup.Text = AgL.XNull(.Rows(0)("ItemInvoiceGroupDesc"))
                TxtVatCommodity.Tag = AgL.XNull(.Rows(0)("VatCommodityCode"))
                TxtVatCommodity.Text = AgL.XNull(.Rows(0)("VatCommodityDesc"))
                TxtTariffHead.Tag = AgL.XNull(.Rows(0)("TariffHead"))
                TxtTariffHead.Text = AgL.XNull(.Rows(0)("TariffHeadDesc"))

                ChkIsSystemDefine.Checked = AgL.VNull(.Rows(0)("IsSystemDefine"))
                LblIsSystemDefine.Text = IIf(AgL.VNull(.Rows(0)("IsSystemDefine")) = 0, "User Define", "System Define")
                ChkIsSystemDefine.Enabled = False


                TxtIsRestrictedinTransaction.Text = IIf(AgL.VNull(.Rows(0)("IsRestricted_InTransaction")) = True, "Yes", "No")

                TxtCustomFields.Tag = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(ClsMain.Temp_NCat.Item, AgL.GcnRead)

                If AgL.XNull(.Rows(0)("CustomFields")) <> "" Then
                    TxtCustomFields.Tag = AgL.XNull(.Rows(0)("CustomFields"))
                End If
                AgCustomGrid1.FrmType = Me.FrmType
                AgCustomGrid1.AgCustom = TxtCustomFields.Tag

                IniGrid()

                If AgL.VNull(.Rows(0)("Cnt")) > 0 Then
                    BtnBOMDetail.ForeColor = Color.Red
                Else
                    BtnBOMDetail.ForeColor = Color.Black
                End If

                AgCustomGrid1.FMoveRecFooterTable(DsTemp.Tables(0))

            End If
        End With

        DsTemp = Nothing

        mQry = " SELECT * FROM ItemSiteDetail " & _
                " WHERE Code = '" & mSearchCode & "' " & _
                " AND Div_Code = '" & AgL.PubDivCode & "' " & _
                " AND Site_Code = '" & AgL.PubSiteCode & "' "
        DsTemp = AgL.FillData(mQry, AgL.GCn)
        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                TxtMinimumStockLevel.Text = AgL.VNull(.Rows(0)("MinimumStockLevel"))
                TxtMaximumStockLevel.Text = AgL.VNull(.Rows(0)("MaximumStockLevel"))
                TxtReOrderStockLevel.Text = AgL.VNull(.Rows(0)("ReOrderStockLevel"))
                TxtPurchQtyAllowedWithoutPO.Text = AgL.VNull(.Rows(0)("PurchQtyAllowedWithoutPO"))
                TxtBinLocation.Text = AgL.XNull(.Rows(0)("BinLocation"))

                TxtIsRequired_LotNo.Text = IIf(AgL.XNull(.Rows(0)("IsRequired_LotNo")) = True, "Yes", "No")
                TxtIsUnitConversionMandatory.Text = IIf(AgL.VNull(.Rows(0)("IsMandatory_UnitConversion")) = True, "Yes", "No")

            End If
        End With

        '-------------------------------------------------------------
        'Image Show
        '-------------------------------------------------------------

        mQry = "Select Im.* " & _
                " From Item_Image Im Where Code='" & mSearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)
        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                If Not IsDBNull(.Rows(0)("Photo")) Then
                    Photo_Byte = DirectCast(.Rows(0)("Photo"), Byte())
                    Show_Picture(PicPhoto, Photo_Byte)
                End If
            End If
        End With

        TxtUnit.AgLastValueTag = ""
        TxtUnit.AgLastValueText = ""
        TxtMeasureUnit.AgLastValueTag = ""
        TxtMeasureUnit.AgLastValueText = ""
        TxtMeasurePerPcs.AgLastValueText = ""
        TxtRate.AgLastValueText = 0
        TxtItemGroup.AgLastValueTag = ""
        TxtItemGroup.AgLastValueText = ""
        TxtItemType.AgLastValueTag = ""
        TxtItemType.AgLastValueText = ""
        TxtItemCategory.AgLastValueTag = ""
        TxtItemCategory.AgLastValueText = ""
        TxtVatCommodity.AgLastValueTag = ""
        TxtVatCommodity.AgLastValueText = ""
        TxtSalesTaxPostingGroup.AgLastValueTag = ""
        TxtSalesTaxPostingGroup.AgLastValueText = ""
        TxtTariffHead.AgLastValueTag = ""
        TxtTariffHead.AgLastValueText = ""
        TxtItemInvoiceGroup.AgLastValueTag = ""
        TxtItemInvoiceGroup.AgLastValueText = ""
        TxtSpecification.AgLastValueText = ""

        TxtMinimumStockLevel.AgLastValueText = 0
        TxtMaximumStockLevel.AgLastValueText = 0
        TxtReOrderStockLevel.AgLastValueText = 0
        TxtBinLocation.AgLastValueText = ""
        TxtIsRequired_LotNo.AgLastValueText = ""
        TxtPurchQtyAllowedWithoutPO.AgLastValueText = 0
        TxtIsUnitConversionMandatory.AgLastValueText = ""

    End Sub

    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        TxtManualCode.Focus()
    End Sub

    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        TxtManualCode.Focus()
    End Sub

    Private Sub Topctrl1_tbPrn() Handles Topctrl1.tbPrn
    End Sub

    Private Sub TxtDescription_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtDescription.KeyDown, TxtManualCode.KeyDown, TxtUnit.KeyDown, TxtMeasureUnit.KeyDown, TxtSalesTaxPostingGroup.KeyDown, TxtItemGroup.KeyDown, TxtItemInvoiceGroup.KeyDown, TxtVatCommodity.KeyDown, TxtTariffHead.KeyDown, TxtItemCategory.KeyDown
        Try
            Select Case sender.Name
                Case TxtDescription.Name
                    If e.KeyCode <> Keys.Enter Then
                        If TxtDescription.AgHelpDataSet Is Nothing Then
                            mQry = "Select Code, Description As Name , Div_Code, ItemType " & _
                                    " From Item Where ItemType in ('" & AgTemplate.ClsMain.ItemType.FinishedMaterial & "','" & AgTemplate.ClsMain.ItemType.RawMaterial & "','" & AgTemplate.ClsMain.ItemType.Other & "','" & AgTemplate.ClsMain.ItemType.SemiFinishedMaterial & "')" & _
                                    " Order By Description"
                            TxtDescription.AgHelpDataSet(2) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case TxtManualCode.Name
                    If e.KeyCode <> Keys.Enter Then
                        If TxtManualCode.AgHelpDataSet Is Nothing Then
                            mQry = "Select Code, ManualCode As ItemCode, Div_Code ,ItemType " & _
                                    " From Item Where ItemType in ('" & AgTemplate.ClsMain.ItemType.FinishedMaterial & "','" & AgTemplate.ClsMain.ItemType.RawMaterial & "','" & AgTemplate.ClsMain.ItemType.Other & "','" & AgTemplate.ClsMain.ItemType.SemiFinishedMaterial & "')" & _
                                    " Order By ManualCode "
                            TxtManualCode.AgHelpDataSet(2) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case TxtUnit.Name
                    If e.KeyCode <> Keys.Enter Then
                        If TxtUnit.AgHelpDataSet Is Nothing Then
                            mQry = "SELECT Code, Code AS Unit FROM Unit "
                            TxtUnit.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case TxtMeasureUnit.Name
                    If e.KeyCode <> Keys.Enter Then
                        If TxtMeasureUnit.AgHelpDataSet Is Nothing Then
                            mQry = "SELECT Code, Code AS Unit FROM Unit "
                            TxtMeasureUnit.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case TxtSalesTaxPostingGroup.Name
                    If e.KeyCode <> Keys.Enter Then
                        If TxtSalesTaxPostingGroup.AgHelpDataSet Is Nothing Then
                            mQry = "SELECT Description as  Code, Description AS PostingGroupSalesTaxItem FROM PostingGroupSalesTaxItem "
                            TxtSalesTaxPostingGroup.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case TxtItemGroup.Name
                    If e.KeyCode = Keys.Insert Then
                        FOpenItemGroupMaster()
                    Else
                        If TxtItemGroup.AgHelpDataSet Is Nothing Then
                            If e.KeyCode <> Keys.Enter Then
                                mQry = " Select I.Code As Code, I.Description As ItemGroup, I.ItemCategory, I.ItemType, IT.Name AS ItemTypeName, IC.Description AS ItemCategoryDesc " & _
                                        " From ItemGroup I " & _
                                        " LEFT JOIN ItemType IT ON IT.Code = I.ItemType " & _
                                        " LEFT JOIN ItemCategory IC ON IC.Code = I.ItemCategory " & _
                                        " Where I.ItemType in ('" & AgTemplate.ClsMain.ItemType.FinishedMaterial & "','" & AgTemplate.ClsMain.ItemType.RawMaterial & "','" & AgTemplate.ClsMain.ItemType.Other & "','" & AgTemplate.ClsMain.ItemType.SemiFinishedMaterial & "')"
                                TxtItemGroup.AgHelpDataSet(4) = AgL.FillData(mQry, AgL.GCn)
                            End If
                        End If
                    End If

                Case TxtItemCategory.Name
                    If e.KeyCode <> Keys.Enter Then
                        If TxtItemCategory.AgHelpDataSet Is Nothing Then
                            mQry = "SELECT Code, Description FROM ItemCategory WHERE ItemType = '" & TxtItemType.Tag & "'  "
                            TxtItemCategory.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case TxtItemInvoiceGroup.Name
                    If e.KeyCode <> Keys.Enter Then
                        If TxtItemInvoiceGroup.AgHelpDataSet Is Nothing Then
                            mQry = "SELECT Code, Description  FROM ItemInvoiceGroup  "
                            TxtItemInvoiceGroup.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case TxtVatCommodity.Name
                    If TxtVatCommodity.AgHelpDataSet Is Nothing Then
                        If e.KeyCode <> Keys.Enter Then
                            mQry = " Select I.Code As Code, I.Description From VatCommodityCode I "
                            TxtVatCommodity.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case TxtTariffHead.Name
                    If TxtTariffHead.AgHelpDataSet Is Nothing Then
                        If e.KeyCode <> Keys.Enter Then
                            mQry = " Select I.Code As Code, I.Description From TariffHead I "
                            TxtTariffHead.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Control_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtItemGroup.Validating
        Dim DtTemp As DataTable = Nothing
        Dim DrTemp As DataRow() = Nothing
        Try
            Select Case sender.NAME
                Case TxtItemGroup.Name
                    If sender.text.ToString.Trim <> "" Then
                        If sender.AgHelpDataSet IsNot Nothing Then
                            DrTemp = sender.AgHelpDataSet.Tables(0).Select("Code = " & AgL.Chk_Text(sender.AgSelectedValue) & "")
                            TxtItemType.Text = AgL.XNull(DrTemp(0)("ItemTypeName"))
                            TxtItemType.Tag = AgL.XNull(DrTemp(0)("ItemType"))
                        End If
                    Else
                        TxtItemType.Text = ""
                        TxtItemType.Tag = ""
                    End If
                    TxtItemCategory.AgHelpDataSet = Nothing
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub FrmYarn_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AgL.WinSetting(Me, 520, 950, 0, 0)
        AgCustomGrid1.FrmType = Me.FrmType
        FManageSystemDefine()
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub TxtManualCode_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        If TxtDescription.Text = "" Then TxtDescription.Text = TxtManualCode.Text
    End Sub

    Private Sub TxtItemCategory_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            If MsgBox("Do you want to save?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Save") = MsgBoxResult.Yes Then
                Topctrl1.FButtonClick(13)
            End If
        End If
    End Sub

    Private Sub FrmFinishedItem_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        If TxtDescription.AgHelpDataSet IsNot Nothing Then TxtDescription.AgHelpDataSet = Nothing
        If TxtManualCode.AgHelpDataSet IsNot Nothing Then TxtManualCode.AgHelpDataSet = Nothing
        If TxtSalesTaxPostingGroup.AgHelpDataSet IsNot Nothing Then TxtSalesTaxPostingGroup.AgHelpDataSet = Nothing
        If TxtUnit.AgHelpDataSet IsNot Nothing Then TxtUnit.AgHelpDataSet = Nothing
        If TxtMeasureUnit.AgHelpDataSet IsNot Nothing Then TxtMeasureUnit.AgHelpDataSet = Nothing
        If TxtItemGroup.AgHelpDataSet IsNot Nothing Then TxtItemGroup.AgHelpDataSet = Nothing
        If TxtItemCategory.AgHelpDataSet IsNot Nothing Then TxtItemCategory.AgHelpDataSet = Nothing
    End Sub

    Private Sub FrmItemMaster_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        TxtItemType.Enabled = False
    End Sub

    Private Sub FrmItemMaster_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        TxtCustomFields.Tag = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(ClsMain.Temp_NCat.Item, AgL.GCn)
        AgCustomGrid1.AgCustom = TxtCustomFields.Tag
        IniGrid()

        If TxtManualCode.AgLastValueText = "" Then
            TxtManualCode.Text = AgL.XNull(AgL.Dman_Execute("Select IsNull(Max(Convert(Numeric,I.ManualCode)),0)+1 AS ManualCode FROM Item I With (NoLock) WHERE Try_Parse(I.ManualCode as BigInt) > 0", AgL.GcnRead).ExecuteScalar)
        Else
            TxtManualCode.Text = (Val(TxtManualCode.AgLastValueText) + 1).ToString
        End If


        TxtDescription.Focus()

        TxtUnit.Tag = TxtUnit.AgLastValueTag
        TxtUnit.Text = TxtUnit.AgLastValueText
        TxtMeasureUnit.Tag = TxtMeasureUnit.AgLastValueTag
        TxtMeasureUnit.Text = TxtMeasureUnit.AgLastValueText
        TxtMeasurePerPcs.Text = TxtMeasurePerPcs.AgLastValueText
        TxtRate.Text = TxtRate.AgLastValueText
        TxtItemGroup.Tag = TxtItemGroup.AgLastValueTag
        TxtItemGroup.Text = TxtItemGroup.AgLastValueText
        TxtItemType.Tag = TxtItemType.AgLastValueTag
        TxtItemType.Text = TxtItemType.AgLastValueText
        TxtItemCategory.Tag = TxtItemCategory.AgLastValueTag
        TxtItemCategory.Text = TxtItemCategory.AgLastValueText
        TxtVatCommodity.Tag = TxtVatCommodity.AgLastValueTag
        TxtVatCommodity.Text = TxtVatCommodity.AgLastValueText
        TxtSalesTaxPostingGroup.Tag = TxtSalesTaxPostingGroup.AgLastValueTag
        TxtSalesTaxPostingGroup.Text = TxtSalesTaxPostingGroup.AgLastValueText
        TxtTariffHead.Tag = TxtTariffHead.AgLastValueTag
        TxtTariffHead.Text = TxtTariffHead.AgLastValueText
        TxtItemInvoiceGroup.Tag = TxtItemInvoiceGroup.AgLastValueTag
        TxtItemInvoiceGroup.Text = TxtItemInvoiceGroup.AgLastValueText
        TxtSpecification.Text = TxtSpecification.AgLastValueText

        TxtMinimumStockLevel.Text = TxtMinimumStockLevel.AgLastValueText
        TxtMaximumStockLevel.Text = TxtMaximumStockLevel.AgLastValueText
        TxtReOrderStockLevel.Text = TxtReOrderStockLevel.AgLastValueText
        TxtBinLocation.Text = TxtBinLocation.AgLastValueText
        TxtIsRequired_LotNo.Text = TxtIsRequired_LotNo.AgLastValueText
        TxtPurchQtyAllowedWithoutPO.Text = TxtPurchQtyAllowedWithoutPO.AgLastValueText
        TxtIsUnitConversionMandatory.Text = TxtIsUnitConversionMandatory.AgLastValueText
        TxtIsRestrictedinTransaction.Text = TxtIsRestrictedinTransaction.AgLastValueText


        ChkIsSystemDefine.Checked = False
        FManageSystemDefine()
    End Sub

    Private Sub FrmItemMaster_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        AgCustomGrid1.Ini_Grid(mSearchCode)
        AgCustomGrid1.SplitGrid = False
    End Sub

    Private Sub BtnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnBrowse.Click, BtnPhotoClear.Click
        If Topctrl1.Mode = "Browse" Then Exit Sub
        Select Case sender.Name
            Case BtnBrowse.Name
                AgL.GetPicture(PicPhoto, Photo_Byte)
                If Photo_Byte.Length > 20480 Then Photo_Byte = Nothing : PicPhoto.Image = Nothing : MsgBox("Image Size Should not be Greater Than 20 KB ")

            Case BtnPhotoClear.Name
                Photo_Byte = Nothing
                PicPhoto.Image = Nothing
        End Select
    End Sub

    Sub Show_Picture(ByVal PicBox As PictureBox, ByVal B As Byte())
        Dim Mem As MemoryStream
        Dim Img As Image

        Mem = New MemoryStream(B)
        Img = Image.FromStream(Mem)
        PicBox.Image = Img
    End Sub

    Sub Update_Picture(ByVal mTable As String, ByVal mColumn As String, ByVal mCondition As String, ByVal ByteArr As Byte())
        If ByteArr Is Nothing Then Exit Sub
        Dim sSQL As String = "Update " & mTable & " Set " & mColumn & "=@pic " & mCondition

        Dim cmd As SqlCommand = New SqlCommand(sSQL, AgL.GCn)
        Dim Pic As SqlParameter = New SqlParameter("@pic", SqlDbType.Image)
        Pic.Value = ByteArr
        cmd.Parameters.Add(Pic)
        cmd.ExecuteNonQuery()
    End Sub

    Private Sub FrmItemMaster_BaseEvent_Save_PostTrans(ByVal SearchCode As String) Handles Me.BaseEvent_Save_PostTrans
        Call Update_Picture("Item_Image", "Photo", "Where Code = '" & mSearchCode & "'", Photo_Byte)
    End Sub

    Private Sub FCreateHelpItemGroup()
        mQry = " Select I.Code As Code, I.Description As ItemGroup, I.ItemCategory, I.ItemType, IT.Name AS ItemTypeName, IC.Description AS ItemCategoryDesc " & _
                " From ItemGroup I " & _
                " LEFT JOIN ItemType IT ON IT.Code = I.ItemType " & _
                " LEFT JOIN ItemCategory IC ON IC.Code = I.ItemCategory "
        TxtItemGroup.AgHelpDataSet(3) = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub FOpenItemGroupMaster()
        Dim DrTemp As DataRow() = Nothing
        Dim bStrCode$ = ""
        bStrCode = AgTemplate.ClsMain.FOpenMaster(Me, "Item Group Master", "")
        FCreateHelpItemGroup()
        DrTemp = TxtItemGroup.AgHelpDataSet.Tables(0).Select("Code = '" & bStrCode & "'")
        TxtItemGroup.Tag = bStrCode
        TxtItemGroup.Text = AgL.XNull(AgL.Dman_Execute("Select Description From ItemGroup Where Code = '" & bStrCode & "'", AgL.GCn).ExecuteScalar)
        TxtItemGroup.Focus()
        SendKeys.Send("{Enter}")
    End Sub

    Private Sub BtnRateConversion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnUnitConversion.Click, BtnBOMDetail.Click
        Select Case sender.Name
            Case BtnUnitConversion.Name
                Dim FrmObj As FrmItemMasterUnitConversion = Nothing
                If AgL.StrCmp(Topctrl1.Mode, "Browse") Then
                    FMoveRecItemUnitConversion(mSearchCode)
                    BtnUnitConversion.Tag.ShowDialog()
                Else
                    FillUnitConversionDetail(True)
                End If

            Case BtnBOMDetail.Name
                Dim FrmObj As FrmItemMasterBOMDetail = Nothing
                If AgL.StrCmp(Topctrl1.Mode, "Browse") Then
                    FMoveRecItemBOMDetail(mSearchCode)
                    BtnBOMDetail.Tag.Text = TxtDescription.Text
                    BtnBOMDetail.Tag.StartPosition = FormStartPosition.CenterParent
                    BtnBOMDetail.Tag.ShowDialog()
                Else
                    FillBOMDetail(True)
                End If

        End Select
    End Sub

    Private Sub FillUnitConversionDetail(ByVal ShowWindow As Boolean)
        If BtnUnitConversion.Tag Is Nothing Then
            FMoveRecItemUnitConversion(mSearchCode)
            If BtnUnitConversion.Tag Is Nothing Then
                BtnUnitConversion.Tag = FunRetNewUnitConversionObject()
            End If
        End If

        BtnUnitConversion.Tag.Dgl1.Readonly = IIf(AgL.StrCmp(Topctrl1.Mode, "Browse"), True, False)
        BtnUnitConversion.Tag.LblItemName.Text = TxtDescription.Text
        BtnUnitConversion.Tag.LblItemName.Tag = mSearchCode
        BtnUnitConversion.Tag.EntryMode = Topctrl1.Mode
        BtnUnitConversion.Tag.Unit = TxtUnit.Text

        If ShowWindow = True Then BtnUnitConversion.Tag.ShowDialog()
    End Sub

    Private Function FunRetNewUnitConversionObject() As Object
        Dim FrmObj As FrmItemMasterUnitConversion
        Try
            FrmObj = New FrmItemMasterUnitConversion
            FrmObj.IniGrid()
            FunRetNewUnitConversionObject = FrmObj
        Catch ex As Exception
            FunRetNewUnitConversionObject = Nothing
            MsgBox(ex.Message)
        End Try
    End Function

    Public Sub FMoveRecItemUnitConversion(ByVal SearchCode As String)
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0
        Try
            BtnUnitConversion.Tag = FunRetNewUnitConversionObject()
            BtnUnitConversion.Tag.Dgl1.Readonly = IIf(AgL.StrCmp(Topctrl1.Mode, "Browse"), True, False)
            mQry = " SELECT U.*, I.Description AS ItemDesc " & _
                    " FROM UnitConversion U " & _
                    " LEFT JOIN Item I ON U.Item = I.Code  " & _
                    " WHERE U.Item = '" & SearchCode & "' "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            With DtTemp
                BtnUnitConversion.Tag.Dgl1.RowCount = 1 : BtnUnitConversion.Tag.Dgl1.Rows.Clear()
                If DtTemp.Rows.Count > 0 Then
                    For I = 0 To DtTemp.Rows.Count - 1
                        BtnUnitConversion.Tag.Dgl1.Rows.Add()
                        BtnUnitConversion.Tag.LblItemName.Text = AgL.XNull(.Rows(I)("ItemDesc"))
                        BtnUnitConversion.Tag.LblItemName.tag = AgL.XNull(.Rows(I)("Item"))
                        BtnUnitConversion.Tag.Dgl1.Item(FrmItemMasterUnitConversion.ColSNo, I).Value = BtnUnitConversion.Tag.Dgl1.Rows.Count - 1
                        BtnUnitConversion.Tag.Dgl1.Item(FrmItemMasterUnitConversion.Col1FromUnit, I).Value = AgL.XNull(.Rows(I)("FromUnit"))
                        BtnUnitConversion.Tag.Dgl1.Item(FrmItemMasterUnitConversion.Col1FromQty, I).Value = AgL.VNull(.Rows(I)("FromQty"))
                        BtnUnitConversion.Tag.Dgl1.Item(FrmItemMasterUnitConversion.Col1ToUnit, I).Value = AgL.XNull(.Rows(I)("ToUnit"))
                        BtnUnitConversion.Tag.Dgl1.Item(FrmItemMasterUnitConversion.Col1ToQty, I).Value = AgL.VNull(.Rows(I)("ToQty"))
                        BtnUnitConversion.Tag.Dgl1.Item(FrmItemMasterUnitConversion.Col1Multiplier, I).Value = AgL.VNull(.Rows(I)("Multiplier"))
                        BtnUnitConversion.Tag.Dgl1.Item(FrmItemMasterUnitConversion.Col1Equal, I).Value = "="

                        BtnUnitConversion.Tag.EntryMode = Topctrl1.Mode
                    Next I
                End If
            End With

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FillBOMDetail(ByVal ShowWindow As Boolean)
        If BtnBOMDetail.Tag Is Nothing Then
            FMoveRecItemBOMDetail(mSearchCode)
            If BtnBOMDetail.Tag Is Nothing Then
                BtnBOMDetail.Tag = FunRetNewBOMDetailObject()
            End If
        End If

        BtnBOMDetail.Tag.Dgl1.Readonly = IIf(AgL.StrCmp(Topctrl1.Mode, "Browse"), True, False)
        BtnBOMDetail.Tag.LblItemName.Text = TxtDescription.Text
        BtnBOMDetail.Tag.LblItemName.Tag = mSearchCode
        BtnBOMDetail.Tag.EntryMode = Topctrl1.Mode
        BtnBOMDetail.Tag.LblUnit.Text = TxtUnit.Text

        If ShowWindow = True Then BtnBOMDetail.Tag.ShowDialog()
    End Sub

    Private Function FunRetNewBOMDetailObject() As Object
        Dim FrmObj As FrmItemMasterBOMDetail
        Try
            FrmObj = New FrmItemMasterBOMDetail
            FrmObj.IniGrid()
            FunRetNewBOMDetailObject = FrmObj
        Catch ex As Exception
            FunRetNewBOMDetailObject = Nothing
            MsgBox(ex.Message)
        End Try
    End Function

    Public Sub FMoveRecItemBOMDetail(ByVal SearchCode As String)
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0
        Try
            BtnBOMDetail.Tag = FunRetNewBOMDetailObject()
            BtnBOMDetail.Tag.Dgl1.Readonly = IIf(AgL.StrCmp(Topctrl1.Mode, "Browse"), True, False)
            mQry = " SELECT BD.*, IB.Description AS BaseItemDesc , I.Description AS ItemDesc , P.Description AS ProcessDesc, " & _
                    " D1.Description AS Dimension1Desc, D2.Description AS Dimension2Desc, isnull(V.Cnt,0) AS Cnt " & _
                    " FROM BomDetail BD " & _
                    " LEFT JOIN Item IB On IB.Code = BD.BaseItem  " & _
                    " LEFT JOIN Process P ON P.NCat = BD.Process  " & _
                    " LEFT JOIN Dimension1 D1 ON D1.Code = BD.Dimension1  " & _
                    " LEFT JOIN Dimension2 D2 ON D2.Code = BD.Dimension2  " & _
                    " LEFT JOIN Item I On I.Code = BD.Item  " & _
                    " LEFT JOIN ( SELECT L.BaseItem, count(*) AS Cnt  FROM BomDetail L GROUP BY L.BaseItem ) V ON V.BaseItem = BD.Item " & _
                    " WHERE BD.BaseItem = '" & SearchCode & "' " & _
                    " ORDER BY BD.Sr "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

            With DtTemp
                BtnBOMDetail.Tag.Dgl1.RowCount = 1 : BtnBOMDetail.Tag.Dgl1.Rows.Clear()
                If DtTemp.Rows.Count > 0 Then
                    For I = 0 To DtTemp.Rows.Count - 1
                        BtnBOMDetail.Tag.Dgl1.Rows.Add()
                        BtnBOMDetail.Tag.LblItemName.Text = AgL.XNull(.Rows(I)("BaseItemDesc"))
                        BtnBOMDetail.Tag.LblItemName.tag = AgL.XNull(.Rows(I)("BaseItem"))
                        BtnBOMDetail.Tag.LblUnit.Text = AgL.XNull(.Rows(I)("BatchUnit"))
                        BtnBOMDetail.Tag.TxtBatchQty.Text = AgL.VNull(.Rows(I)("BatchQty"))
                        BtnBOMDetail.Tag.Dgl1.Item(FrmItemMasterBOMDetail.ColSNo, I).Value = BtnBOMDetail.Tag.Dgl1.Rows.Count - 1
                        BtnBOMDetail.Tag.Dgl1.Item(FrmItemMasterBOMDetail.Col1Process, I).Value = AgL.XNull(.Rows(I)("ProcessDesc"))
                        BtnBOMDetail.Tag.Dgl1.Item(FrmItemMasterBOMDetail.Col1Process, I).Tag = AgL.XNull(.Rows(I)("Process"))
                        BtnBOMDetail.Tag.Dgl1.Item(FrmItemMasterBOMDetail.Col1Item, I).Value = AgL.XNull(.Rows(I)("ItemDesc"))
                        BtnBOMDetail.Tag.Dgl1.Item(FrmItemMasterBOMDetail.Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                        BtnBOMDetail.Tag.Dgl1.Item(FrmItemMasterBOMDetail.Col1Dimension1, I).Value = AgL.XNull(.Rows(I)("Dimension1Desc"))
                        BtnBOMDetail.Tag.Dgl1.Item(FrmItemMasterBOMDetail.Col1Dimension1, I).Tag = AgL.XNull(.Rows(I)("Dimension1"))
                        BtnBOMDetail.Tag.Dgl1.Item(FrmItemMasterBOMDetail.Col1Dimension2, I).Value = AgL.XNull(.Rows(I)("Dimension2Desc"))
                        BtnBOMDetail.Tag.Dgl1.Item(FrmItemMasterBOMDetail.Col1Dimension2, I).Tag = AgL.XNull(.Rows(I)("Dimension2"))
                        BtnBOMDetail.Tag.Dgl1.Item(FrmItemMasterBOMDetail.Col1Qty, I).Value = AgL.VNull(.Rows(I)("Qty"))
                        BtnBOMDetail.Tag.Dgl1.Item(FrmItemMasterBOMDetail.Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                        BtnBOMDetail.Tag.Dgl1.Item(FrmItemMasterBOMDetail.Col1WastagePer, I).Value = AgL.VNull(.Rows(I)("WastagePer"))
                        If AgL.VNull(.Rows(I)("Cnt")) > 0 Then
                            BtnBOMDetail.Tag.Dgl1.Item(FrmItemMasterBOMDetail.Col1BtnBOMDetail, I).Style.ForeColor = Color.Red
                        End If
                        BtnBOMDetail.Tag.EntryMode = Topctrl1.Mode
                    Next I
                End If
            End With

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmItemMasterNew_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Photo_Byte = Nothing
        PicPhoto.Image = Nothing
        BtnUnitConversion.Tag = Nothing
        BtnBOMDetail.Tag = Nothing
    End Sub

    Private Sub FrmItemMaster_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
        Passed = FRestrictSystemDefine()
    End Sub

    Private Sub FrmItemMaster_BaseEvent_Topctrl_tbDel(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbDel
        Passed = FRestrictSystemDefine()
    End Sub

    Private Sub ChkIsSystemDefine_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkIsSystemDefine.Click
        FManageSystemDefine()
    End Sub

    Private Sub FManageSystemDefine()
        If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Then
            ChkIsSystemDefine.Visible = True
            ChkIsSystemDefine.Enabled = True
        Else
            ChkIsSystemDefine.Visible = False
            ChkIsSystemDefine.Enabled = False
        End If

        If ChkIsSystemDefine.Checked Then
            LblIsSystemDefine.Text = "System Define"
        Else
            LblIsSystemDefine.Text = "User Define"
        End If
    End Sub

    Private Function FRestrictSystemDefine() As Boolean
        If ChkIsSystemDefine.Checked = True Then
            If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Then
                If MsgBox("This is a System Define Item.Do You Want To Proceed...?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    Topctrl1.FButtonClick(14, True)
                    FRestrictSystemDefine = False
                    Exit Function
                End If
            Else
                MsgBox("Can't Edit System Define Items...!", MsgBoxStyle.Information) : Topctrl1.FButtonClick(14, True)
                FRestrictSystemDefine = False
                Exit Function
            End If
        End If
        FManageSystemDefine()
        FRestrictSystemDefine = True
    End Function
End Class

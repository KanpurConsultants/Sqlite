Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SQLite
Imports System.IO
Public Class FrmItemMaster_Cloth
    Inherits AgTemplate.TempMaster
    Dim mQry$
    Friend WithEvents ChkIsSystemDefine As System.Windows.Forms.CheckBox
    Public WithEvents LblIsSystemDefine As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents PnlRateType As Panel
    Dim Photo_Byte As Byte()
    Public Const ColSNo As String = "SNo"
    Public WithEvents DGLRateType As New AgControls.AgDataGrid
    Public Const Col1RateType As String = "Rate Type"
    Public Const Col1Margin As String = "Margin"
    Public WithEvents Label2 As Label
    Public WithEvents TxtSpecification As AgControls.AgTextBox
    Public WithEvents Label4 As Label
    Public WithEvents TxtHsn As AgControls.AgTextBox
    Public WithEvents Label9 As Label
    Public Const Col1Rate As String = "Rate"


    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)
    End Sub

#Region "Designer Code"
    Private Sub InitializeComponent()
        Me.TxtCustomFields = New AgControls.AgTextBox()
        Me.PicPhoto = New System.Windows.Forms.PictureBox()
        Me.BtnBrowse = New System.Windows.Forms.Button()
        Me.BtnPhotoClear = New System.Windows.Forms.Button()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.LblItemCategory = New System.Windows.Forms.Label()
        Me.PnlCustomGrid = New System.Windows.Forms.Panel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TxtItemType = New AgControls.AgTextBox()
        Me.TxtRate = New AgControls.AgTextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TxtItemCategory = New AgControls.AgTextBox()
        Me.TxtItemGroup = New AgControls.AgTextBox()
        Me.LblItemGroup = New System.Windows.Forms.Label()
        Me.TxtSalesTaxPostingGroup = New AgControls.AgTextBox()
        Me.LblSalesTaxPostingGroup = New System.Windows.Forms.Label()
        Me.LblManualCodeReq = New System.Windows.Forms.Label()
        Me.TxtManualCode = New AgControls.AgTextBox()
        Me.LblManualCode = New System.Windows.Forms.Label()
        Me.TxtUnit = New AgControls.AgTextBox()
        Me.LblUnit = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TxtDescription = New AgControls.AgTextBox()
        Me.LblDescription = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.LblMaterialPlanForFollowingItems = New System.Windows.Forms.LinkLabel()
        Me.BtnUnitConversion = New System.Windows.Forms.Button()
        Me.BtnBOMDetail = New System.Windows.Forms.Button()
        Me.ChkIsSystemDefine = New System.Windows.Forms.CheckBox()
        Me.LblIsSystemDefine = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.PnlRateType = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtSpecification = New AgControls.AgTextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TxtHsn = New AgControls.AgTextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.GrpUP.SuspendLayout()
        Me.GBoxEntryType.SuspendLayout()
        Me.GBoxMoveToLog.SuspendLayout()
        Me.GBoxApprove.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GBoxDivision.SuspendLayout()
        CType(Me.DTMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicPhoto, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.TxtCustomFields.Location = New System.Drawing.Point(473, 246)
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
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(9, 199)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(64, 16)
        Me.Label15.TabIndex = 1056
        Me.Label15.Text = "Item Type"
        '
        'LblItemCategory
        '
        Me.LblItemCategory.AutoSize = True
        Me.LblItemCategory.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblItemCategory.Location = New System.Drawing.Point(9, 59)
        Me.LblItemCategory.Name = "LblItemCategory"
        Me.LblItemCategory.Size = New System.Drawing.Size(89, 16)
        Me.LblItemCategory.TabIndex = 1054
        Me.LblItemCategory.Text = "Item Category"
        '
        'PnlCustomGrid
        '
        Me.PnlCustomGrid.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlCustomGrid.Location = New System.Drawing.Point(17, 246)
        Me.PnlCustomGrid.Name = "PnlCustomGrid"
        Me.PnlCustomGrid.Size = New System.Drawing.Size(429, 190)
        Me.PnlCustomGrid.TabIndex = 17
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(115, 81)
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
        Me.Label8.Location = New System.Drawing.Point(115, 181)
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
        Me.Label5.Location = New System.Drawing.Point(115, 161)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(10, 7)
        Me.Label5.TabIndex = 1047
        Me.Label5.Text = "Ä"
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
        Me.TxtItemType.Location = New System.Drawing.Point(131, 199)
        Me.TxtItemType.MaxLength = 20
        Me.TxtItemType.Name = "TxtItemType"
        Me.TxtItemType.Size = New System.Drawing.Size(414, 18)
        Me.TxtItemType.TabIndex = 10
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
        Me.TxtRate.Location = New System.Drawing.Point(400, 159)
        Me.TxtRate.MaxLength = 20
        Me.TxtRate.Name = "TxtRate"
        Me.TxtRate.Size = New System.Drawing.Size(145, 18)
        Me.TxtRate.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(321, 159)
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
        Me.TxtItemCategory.Location = New System.Drawing.Point(131, 59)
        Me.TxtItemCategory.MaxLength = 20
        Me.TxtItemCategory.Name = "TxtItemCategory"
        Me.TxtItemCategory.Size = New System.Drawing.Size(414, 18)
        Me.TxtItemCategory.TabIndex = 1
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
        Me.TxtItemGroup.Location = New System.Drawing.Point(131, 79)
        Me.TxtItemGroup.MaxLength = 20
        Me.TxtItemGroup.Name = "TxtItemGroup"
        Me.TxtItemGroup.Size = New System.Drawing.Size(414, 18)
        Me.TxtItemGroup.TabIndex = 2
        '
        'LblItemGroup
        '
        Me.LblItemGroup.AutoSize = True
        Me.LblItemGroup.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblItemGroup.Location = New System.Drawing.Point(9, 79)
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
        Me.TxtSalesTaxPostingGroup.Location = New System.Drawing.Point(131, 179)
        Me.TxtSalesTaxPostingGroup.MaxLength = 20
        Me.TxtSalesTaxPostingGroup.Name = "TxtSalesTaxPostingGroup"
        Me.TxtSalesTaxPostingGroup.Size = New System.Drawing.Size(177, 18)
        Me.TxtSalesTaxPostingGroup.TabIndex = 8
        '
        'LblSalesTaxPostingGroup
        '
        Me.LblSalesTaxPostingGroup.AutoSize = True
        Me.LblSalesTaxPostingGroup.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblSalesTaxPostingGroup.Location = New System.Drawing.Point(9, 179)
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
        Me.LblManualCodeReq.Location = New System.Drawing.Point(115, 101)
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
        Me.TxtManualCode.Location = New System.Drawing.Point(131, 99)
        Me.TxtManualCode.MaxLength = 20
        Me.TxtManualCode.Name = "TxtManualCode"
        Me.TxtManualCode.Size = New System.Drawing.Size(294, 18)
        Me.TxtManualCode.TabIndex = 3
        '
        'LblManualCode
        '
        Me.LblManualCode.AutoSize = True
        Me.LblManualCode.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblManualCode.Location = New System.Drawing.Point(9, 99)
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
        Me.TxtUnit.Location = New System.Drawing.Point(131, 159)
        Me.TxtUnit.MaxLength = 20
        Me.TxtUnit.Name = "TxtUnit"
        Me.TxtUnit.Size = New System.Drawing.Size(177, 18)
        Me.TxtUnit.TabIndex = 6
        '
        'LblUnit
        '
        Me.LblUnit.AutoSize = True
        Me.LblUnit.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblUnit.Location = New System.Drawing.Point(9, 159)
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
        Me.Label1.Location = New System.Drawing.Point(115, 143)
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
        Me.TxtDescription.Location = New System.Drawing.Point(131, 139)
        Me.TxtDescription.MaxLength = 255
        Me.TxtDescription.Name = "TxtDescription"
        Me.TxtDescription.Size = New System.Drawing.Size(414, 18)
        Me.TxtDescription.TabIndex = 5
        '
        'LblDescription
        '
        Me.LblDescription.AutoSize = True
        Me.LblDescription.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDescription.Location = New System.Drawing.Point(9, 139)
        Me.LblDescription.Name = "LblDescription"
        Me.LblDescription.Size = New System.Drawing.Size(71, 16)
        Me.LblDescription.TabIndex = 1036
        Me.LblDescription.Text = "Item Name"
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
        Me.Panel1.Visible = False
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
        Me.BtnUnitConversion.Visible = False
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
        Me.ChkIsSystemDefine.Location = New System.Drawing.Point(431, 101)
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
        Me.LblIsSystemDefine.Location = New System.Drawing.Point(445, 100)
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
        Me.Label12.Location = New System.Drawing.Point(115, 62)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(10, 7)
        Me.Label12.TabIndex = 1060
        Me.Label12.Text = "Ä"
        '
        'PnlRateType
        '
        Me.PnlRateType.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlRateType.Location = New System.Drawing.Point(563, 58)
        Me.PnlRateType.Name = "PnlRateType"
        Me.PnlRateType.Size = New System.Drawing.Size(286, 139)
        Me.PnlRateType.TabIndex = 11
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(115, 122)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(10, 7)
        Me.Label2.TabIndex = 1063
        Me.Label2.Text = "Ä"
        '
        'TxtSpecification
        '
        Me.TxtSpecification.AgAllowUserToEnableMasterHelp = False
        Me.TxtSpecification.AgLastValueTag = Nothing
        Me.TxtSpecification.AgLastValueText = Nothing
        Me.TxtSpecification.AgMandatory = True
        Me.TxtSpecification.AgMasterHelp = True
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
        Me.TxtSpecification.Location = New System.Drawing.Point(131, 119)
        Me.TxtSpecification.MaxLength = 255
        Me.TxtSpecification.Name = "TxtSpecification"
        Me.TxtSpecification.Size = New System.Drawing.Size(414, 18)
        Me.TxtSpecification.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(10, 119)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(82, 16)
        Me.Label4.TabIndex = 1062
        Me.Label4.Text = "Specification"
        '
        'TxtHsn
        '
        Me.TxtHsn.AgAllowUserToEnableMasterHelp = False
        Me.TxtHsn.AgLastValueTag = Nothing
        Me.TxtHsn.AgLastValueText = Nothing
        Me.TxtHsn.AgMandatory = False
        Me.TxtHsn.AgMasterHelp = False
        Me.TxtHsn.AgNumberLeftPlaces = 8
        Me.TxtHsn.AgNumberNegetiveAllow = False
        Me.TxtHsn.AgNumberRightPlaces = 0
        Me.TxtHsn.AgPickFromLastValue = False
        Me.TxtHsn.AgRowFilter = ""
        Me.TxtHsn.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtHsn.AgSelectedValue = Nothing
        Me.TxtHsn.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtHsn.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtHsn.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtHsn.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtHsn.Location = New System.Drawing.Point(400, 179)
        Me.TxtHsn.MaxLength = 8
        Me.TxtHsn.Name = "TxtHsn"
        Me.TxtHsn.Size = New System.Drawing.Size(145, 18)
        Me.TxtHsn.TabIndex = 9
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(321, 179)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(69, 16)
        Me.Label9.TabIndex = 1065
        Me.Label9.Text = "HSN Code"
        '
        'FrmItemMaster_Cloth
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(944, 492)
        Me.Controls.Add(Me.TxtHsn)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtSpecification)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.PnlRateType)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.LblIsSystemDefine)
        Me.Controls.Add(Me.ChkIsSystemDefine)
        Me.Controls.Add(Me.BtnBOMDetail)
        Me.Controls.Add(Me.BtnUnitConversion)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.PnlCustomGrid)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label5)
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
        Me.Name = "FrmItemMaster_Cloth"
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
        Me.Controls.SetChildIndex(Me.Label5, 0)
        Me.Controls.SetChildIndex(Me.Label8, 0)
        Me.Controls.SetChildIndex(Me.Label6, 0)
        Me.Controls.SetChildIndex(Me.Label15, 0)
        Me.Controls.SetChildIndex(Me.PnlCustomGrid, 0)
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
        Me.Controls.SetChildIndex(Me.PnlRateType, 0)
        Me.Controls.SetChildIndex(Me.Label4, 0)
        Me.Controls.SetChildIndex(Me.TxtSpecification, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.Label9, 0)
        Me.Controls.SetChildIndex(Me.TxtHsn, 0)
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
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents TxtCustomFields As AgControls.AgTextBox
    Public WithEvents PicPhoto As System.Windows.Forms.PictureBox
    Public WithEvents BtnBrowse As System.Windows.Forms.Button
    Public WithEvents BtnPhotoClear As System.Windows.Forms.Button
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents LblItemCategory As System.Windows.Forms.Label
    Public WithEvents PnlCustomGrid As System.Windows.Forms.Panel
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
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
    Public WithEvents Panel1 As System.Windows.Forms.Panel
    Public WithEvents LblMaterialPlanForFollowingItems As System.Windows.Forms.LinkLabel
    Public WithEvents AgCustomGrid1 As New AgCustomFields.AgCustomGrid
    Public WithEvents BtnUnitConversion As System.Windows.Forms.Button
    Public WithEvents BtnBOMDetail As System.Windows.Forms.Button
#End Region

    Private Sub FrmItemMasterNew_BaseEvent_ApproveDeletion_InTrans(ByVal SearchCode As String, ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand) Handles Me.BaseEvent_ApproveDeletion_InTrans
        mQry = "DELETE FROM RateListDetail WHERE Code = '" & mSearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = "DELETE FROM RateList WHERE Code = '" & mSearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

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
            mQry = "Select count(*) From Item Where ManualCode ='" & TxtManualCode.Text & "' And Code <>'" & mInternalCode & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Short Name Already Exist!")

            mQry = "Select count(*) From Item Where Description='" & TxtDescription.Text & "' And Code <> '" & mInternalCode & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")
        End If
    End Sub

    Private Sub FrmYarn_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mConStr$ = " Where H.ItemType In ('" & AgTemplate.ClsMain.ItemType.FinishedMaterial & "','" & AgTemplate.ClsMain.ItemType.RawMaterial & "','" & AgTemplate.ClsMain.ItemType.Other & "','" & AgTemplate.ClsMain.ItemType.SemiFinishedMaterial & "') "
        mQry = "Select H.Code As SearchCode " &
                " From Item H " & mConStr &
                " Order By H.Description "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Public Sub FrmYarn_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mConStr$ = " Where I.ItemType In ('" & AgTemplate.ClsMain.ItemType.FinishedMaterial & "','" & AgTemplate.ClsMain.ItemType.RawMaterial & "','" & AgTemplate.ClsMain.ItemType.Other & "','" & AgTemplate.ClsMain.ItemType.SemiFinishedMaterial & "')  "
        AgL.PubFindQry = "SELECT I.Code, I.ManualCode as [Item Code], I.Description [Item Description],I.Specification, " &
                        " IG.Description AS [Item Group], IC.Description AS [Item Category], IT.Name AS [Item Type], I.Unit " &
                        " FROM Item I " &
                        " LEFT JOIN ItemGroup IG ON IG.Code = I.ItemGroup " &
                        " LEFT JOIN ItemCategory IC ON IC.Code = I.ItemCategory " &
                        " LEFT JOIN ItemType IT ON IT.Code = I.ItemType " &
                        "  " & mConStr
        AgL.PubFindQryOrdBy = "[Item Description]"
    End Sub

    Private Sub FrmYarn_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "Item"
        LogTableName = "Item_LOG"

        PrimaryField = "Code"

        AgL.AddAgDataGrid(AgCustomGrid1, PnlCustomGrid)

        AgCustomGrid1.AgLibVar = AgL
        AgCustomGrid1.SplitGrid = True
        AgCustomGrid1.MnuText = Me.Name
    End Sub

    Private Sub FrmYarn_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand) Handles Me.BaseEvent_Save_InTrans
        mQry = "UPDATE Item " &
                " SET " &
                " ManualCode = " & AgL.Chk_Text(TxtManualCode.Text) & ", " &
                " Specification = " & AgL.Chk_Text(TxtSpecification.Text) & ", " &
                " Description = " & AgL.Chk_Text(TxtDescription.Text) & ", " &
                " Hsn = " & AgL.Chk_Text(TxtHsn.Text) & ", " &
                " Unit = " & AgL.Chk_Text(TxtUnit.Text) & ", " &
                " Rate = " & Val(TxtRate.Text) & ", " &
                " ItemGroup = " & AgL.Chk_Text(TxtItemGroup.AgSelectedValue) & ", " &
                " ItemCategory = " & AgL.Chk_Text(TxtItemCategory.Tag) & ", " &
                " ItemType = " & AgL.Chk_Text(TxtItemType.Tag) & ", " &
                " ServiceTaxYN = 'N', " &
                " StockYN = 1, " &
                " IsSystemDefine = " & Val(IIf(ChkIsSystemDefine.Checked, 1, 0)) & ", " &
                " SalesTaxPostingGroup = " & AgL.Chk_Text(TxtSalesTaxPostingGroup.Text) & ", " &
                " CustomFields = " & AgL.Chk_Text(TxtCustomFields.Tag) & " " &
                " " & AgCustomGrid1.FFooterTableUpdateStr() & " " &
                " Where Code = '" & SearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        Call FPostRateInRateList(Conn, Cmd)


        If BtnUnitConversion.Tag IsNot Nothing Then
            Call FSaveUnitConversion(Conn, Cmd)
        End If

        If BtnBOMDetail.Tag IsNot Nothing Then
            Call FSaveBOMDetail(Conn, Cmd)
        End If

        'mQry = "Delete From Item_Image Where Code = '" & mSearchCode & "'"
        'AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        'mQry = "Insert Into Item_Image(Code, Photo) Values('" & mSearchCode & "', Null)"
        'AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        TxtManualCode.AgLastValueText = TxtManualCode.Text
        TxtUnit.AgLastValueTag = TxtUnit.Tag
        TxtUnit.AgLastValueText = TxtUnit.Text
        TxtRate.AgLastValueText = TxtRate.Text
        TxtItemGroup.AgLastValueTag = TxtItemGroup.Tag
        TxtItemGroup.AgLastValueText = TxtItemGroup.Text
        TxtItemType.AgLastValueTag = TxtItemType.Tag
        TxtItemType.AgLastValueText = TxtItemType.Text
        TxtItemCategory.AgLastValueTag = TxtItemCategory.Tag
        TxtItemCategory.AgLastValueText = TxtItemCategory.Text
        TxtSalesTaxPostingGroup.AgLastValueTag = TxtSalesTaxPostingGroup.Tag
        TxtSalesTaxPostingGroup.AgLastValueText = TxtSalesTaxPostingGroup.Text


    End Sub


    Private Sub FPostRateInRateList(ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand)
        Dim bRateListCode$ = ""
        Dim I As Integer, mSr As Integer

        mQry = "DELETE FROM RateList WHERE Code = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "DELETE FROM RateListDetail WHERE Code = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        'bRateListCode = AgL.GetMaxId("RateList", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 8, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)

        mQry = " INSERT INTO RateList(Code, WEF, RateType, EntryBy, EntryDate, EntryType, " &
                " EntryStatus, Status, Div_Code) " &
                " VALUES (" & AgL.Chk_Text(mSearchCode) & ", " & AgL.Chk_Date(AgL.PubLoginDate) & ",	" &
                " NULL,	" & AgL.Chk_Text(AgL.PubUserName) & ", " & AgL.Chk_Date(AgL.PubLoginDate) & ", " &
                " " & AgL.Chk_Text(Topctrl1.Mode) & ", 'Open', " & AgL.Chk_Text(AgTemplate.ClsMain.EntryStatus.Active) & ", " &
                " '" & TxtDivision.AgSelectedValue & "')"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "INSERT INTO RateListDetail(Code, Sr, WEF, Item, RateType, Rate) " &
              " VALUES (" & AgL.Chk_Text(mSearchCode) & ", " &
              " 0, " & AgL.Chk_Date(AgL.PubStartDate) & ", " &
              " " & AgL.Chk_Text(mSearchCode) & ", " &
              " NULL, " & Val(TxtRate.Text) & " ) "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        For I = 0 To DGLRateType.RowCount - 1
            If DGLRateType.Item(Col1RateType, I).Value <> "" Then
                mSr += 1

                mQry = "INSERT INTO RateListDetail(Code, Sr, WEF, Item, RateType, Rate) " &
              " VALUES (" & AgL.Chk_Text(mSearchCode) & ", " &
              " " & mSr & ", " & AgL.Chk_Date(AgL.PubStartDate) & ", " &
              " " & AgL.Chk_Text(mSearchCode) & ", " &
              " " & AgL.Chk_Text(DGLRateType.Item(Col1RateType, I).Tag) & ", " & Val(DGLRateType.Item(Col1Rate, I).Value) & " ) "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If
        Next



    End Sub

    Private Sub FSaveUnitConversion(ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand)
        Dim I As Integer
        mQry = "DELETE FROM UnitConversion WHERE Item = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        If BtnUnitConversion.Tag IsNot Nothing Then
            With BtnUnitConversion.Tag.Dgl1
                For I = 0 To .Rows.Count - 1
                    If .Item(FrmItemMasterUnitConversion.Col1FromUnit, I).Value <> "" Then
                        mQry = " INSERT INTO UnitConversion ( Item,FromUnit,ToUnit,FromQty,ToQty,Multiplier,EntryBy,EntryDate,EntryType,EntryStatus, " &
                                " Status,Div_Code ) " &
                                " VALUES ( " & AgL.Chk_Text(mSearchCode) & ", " &
                                " " & AgL.Chk_Text(.Item(FrmItemMasterUnitConversion.Col1FromUnit, I).Value) & ", " &
                                " " & AgL.Chk_Text(.Item(FrmItemMasterUnitConversion.Col1ToUnit, I).Value) & ", " &
                                " " & Val(.Item(FrmItemMasterUnitConversion.Col1FromQty, I).Value) & ", " &
                                " " & Val(.Item(FrmItemMasterUnitConversion.Col1ToQty, I).Value) & ", " &
                                " " & Val(.Item(FrmItemMasterUnitConversion.Col1Multiplier, I).Value) & ", " &
                                " '" & AgL.PubUserName & "'," & AgL.Chk_Text(AgL.PubLoginDate) & ",	'" & Topctrl1.Mode & "', " &
                                " 'Open',  '" & AgTemplate.ClsMain.EntryStatus.Active & "' , '" & TxtDivision.AgSelectedValue & "' ) "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    End If
                Next
            End With
        End If
    End Sub

    Private Sub FSaveBOMDetail(ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand)
        Dim I As Integer
        mQry = "DELETE FROM BOMDetail WHERE BaseItem = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        If BtnBOMDetail.Tag IsNot Nothing Then
            With BtnBOMDetail.Tag.Dgl1
                For I = 0 To .Rows.Count - 1
                    If .Item(FrmItemMasterBOMDetail.Col1Item, I).Value <> "" Then
                        mQry = " INSERT INTO BomDetail ( Sr, Item, Qty, Process, Dimension1, Dimension2, " &
                                " Unit,WastagePer, BatchQty, BatchUnit, BaseItem ) " &
                                " VALUES ( " & I + 1 & "," &
                                " " & AgL.Chk_Text(.Item(FrmItemMasterBOMDetail.Col1Item, I).tag) & ", " &
                                " " & Val(.Item(FrmItemMasterBOMDetail.Col1Qty, I).Value) & ", " &
                                " " & AgL.Chk_Text(.Item(FrmItemMasterBOMDetail.Col1Process, I).tag) & ", " &
                                " " & AgL.Chk_Text(.Item(FrmItemMasterBOMDetail.Col1Dimension1, I).tag) & ", " &
                                " " & AgL.Chk_Text(.Item(FrmItemMasterBOMDetail.Col1Dimension2, I).tag) & ", " &
                                " " & AgL.Chk_Text(.Item(FrmItemMasterBOMDetail.Col1Unit, I).Value) & ", " &
                                " " & Val(.Item(FrmItemMasterBOMDetail.Col1WastagePer, I).Value) & ", " &
                                " " & Val(BtnBOMDetail.Tag.TxtBatchQty.Text) & ", " &
                                " " & AgL.Chk_Text(BtnBOMDetail.Tag.LblUnit.Text) & ", " &
                                " " & AgL.Chk_Text(mSearchCode) & "	) "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    End If
                Next
            End With
        End If
    End Sub

    Private Sub FrmQuality1_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DsTemp As DataSet
        mQry = "Select I.*, Ig.Description As ItemGroupDesc, IC.Description As ItemCategoryDesc, " &
                " IT.Name AS ItemTypeName, IfNull(V.Cnt,0) AS Cnt " &
                " From Item I " &
                " LEFT JOIN ItemGroup Ig ON I.ItemGroup = IG.Code " &
                " LEFT JOIN ItemCategory IC ON IC.Code = I.ItemCategory " &
                " LEFT JOIN ItemType IT ON IT.Code = I.ItemType " &
                " LEFT JOIN ( SELECT L.BaseItem, count(*) AS Cnt  FROM BomDetail L GROUP BY L.BaseItem ) V ON V.BaseItem = I.Code " &
                " Where I.Code ='" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                mInternalCode = AgL.XNull(.Rows(0)("Code"))
                TxtManualCode.Text = AgL.XNull(.Rows(0)("ManualCode"))
                TxtDescription.Text = AgL.XNull(.Rows(0)("Description"))
                TxtSpecification.Text = AgL.XNull(.Rows(0)("Specification"))
                TxtHsn.Text = AgL.XNull(.Rows(0)("Hsn"))
                TxtUnit.Text = AgL.XNull(.Rows(0)("Unit"))
                TxtRate.Text = AgL.VNull(.Rows(0)("Rate"))
                TxtRate.Tag = AgL.VNull(.Rows(0)("Rate"))
                TxtItemGroup.Tag = AgL.XNull(.Rows(0)("ItemGroup"))
                TxtItemGroup.Text = AgL.XNull(.Rows(0)("ItemGroupDesc"))
                TxtItemCategory.Text = AgL.XNull(.Rows(0)("ItemCategoryDesc"))
                TxtItemCategory.Tag = AgL.XNull(.Rows(0)("ItemCategory"))
                TxtItemType.Text = AgL.XNull(.Rows(0)("ItemTypeName"))
                TxtItemType.Tag = AgL.XNull(.Rows(0)("ItemType"))
                TxtSalesTaxPostingGroup.Text = AgL.XNull(.Rows(0)("SalesTaxPostingGroup"))

                ChkIsSystemDefine.Checked = AgL.VNull(.Rows(0)("IsSystemDefine"))
                LblIsSystemDefine.Text = IIf(AgL.VNull(.Rows(0)("IsSystemDefine")) = 0, "User Define", "System Define")
                ChkIsSystemDefine.Enabled = False

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



                Dim I As Integer
                mQry = " Select  H.Code, H.Description, H.Margin, L.Rate 
                        From RateType H 
                        Left join RateListDetail L on L.RateType = H.Code And L.Item='" & SearchCode & "' 
                        Order By H.Sr "
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    DGLRateType.RowCount = 1
                    DGLRateType.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            DGLRateType.Rows.Add()
                            DGLRateType.Item(ColSNo, I).Value = DGLRateType.Rows.Count - 1
                            DGLRateType.Item(Col1RateType, I).Tag = AgL.XNull(.Rows(I)("Code"))
                            DGLRateType.Item(Col1RateType, I).Value = AgL.XNull(.Rows(I)("Description"))
                            DGLRateType.Item(Col1Margin, I).Value = Format(AgL.VNull(.Rows(I)("Margin")), "0.00")
                            DGLRateType.Item(Col1Rate, I).Value = Format(AgL.VNull(.Rows(I)("Rate")), "0.00")
                        Next I
                    End If
                End With

                AgCustomGrid1.FMoveRecFooterTable(DsTemp.Tables(0))
            End If
        End With






        DsTemp = Nothing


        '-------------------------------------------------------------
        'Image Show
        '-------------------------------------------------------------

        'mQry = "Select Im.* " &
        '        " From Item_Image Im Where Code='" & mSearchCode & "'"
        'DsTemp = AgL.FillData(mQry, AgL.GCn)
        'With DsTemp.Tables(0)
        '    If .Rows.Count > 0 Then
        '        If Not IsDBNull(.Rows(0)("Photo")) Then
        '            Photo_Byte = DirectCast(.Rows(0)("Photo"), Byte())
        '            Show_Picture(PicPhoto, Photo_Byte)
        '        End If
        '    End If
        'End With

        TxtUnit.AgLastValueTag = ""
        TxtUnit.AgLastValueText = ""
        TxtRate.AgLastValueText = 0
        TxtItemGroup.AgLastValueTag = ""
        TxtItemGroup.AgLastValueText = ""
        TxtItemType.AgLastValueTag = ""
        TxtItemType.AgLastValueText = ""
        TxtItemCategory.AgLastValueTag = ""
        TxtItemCategory.AgLastValueText = ""
        TxtSalesTaxPostingGroup.AgLastValueTag = ""
        TxtSalesTaxPostingGroup.AgLastValueText = ""

    End Sub

    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        TxtItemCategory.Focus()
    End Sub

    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        TxtItemCategory.Focus()
    End Sub

    Private Sub Topctrl1_tbPrn() Handles Topctrl1.tbPrn
    End Sub

    Private Sub TxtDescription_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtDescription.KeyDown, TxtManualCode.KeyDown, TxtUnit.KeyDown, TxtSalesTaxPostingGroup.KeyDown, TxtItemGroup.KeyDown, TxtItemCategory.KeyDown
        Try
            Select Case sender.Name
                Case TxtDescription.Name
                    If e.KeyCode <> Keys.Enter Then
                        If TxtDescription.AgHelpDataSet Is Nothing Then
                            mQry = "Select Code, Description As Name , Div_Code, ItemType " &
                                    " From Item Where ItemType in ('" & AgTemplate.ClsMain.ItemType.FinishedMaterial & "','" & AgTemplate.ClsMain.ItemType.RawMaterial & "','" & AgTemplate.ClsMain.ItemType.Other & "','" & AgTemplate.ClsMain.ItemType.SemiFinishedMaterial & "')" &
                                    " Order By Description"
                            TxtDescription.AgHelpDataSet(2) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case TxtManualCode.Name
                    If e.KeyCode <> Keys.Enter Then
                        If TxtManualCode.AgHelpDataSet Is Nothing Then
                            mQry = "Select Code, ManualCode As ItemCode, Div_Code ,ItemType " &
                                    " From Item Where ItemType in ('" & AgTemplate.ClsMain.ItemType.FinishedMaterial & "','" & AgTemplate.ClsMain.ItemType.RawMaterial & "','" & AgTemplate.ClsMain.ItemType.Other & "','" & AgTemplate.ClsMain.ItemType.SemiFinishedMaterial & "')" &
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
                                mQry = " Select I.Code As Code, I.Description As ItemGroup, I.ItemCategory, I.ItemType, IT.Name AS ItemTypeName, IC.Description AS ItemCategoryDesc " &
                                        " From ItemGroup I " &
                                        " LEFT JOIN ItemType IT ON IT.Code = I.ItemType " &
                                        " LEFT JOIN ItemCategory IC ON IC.Code = I.ItemCategory " &
                                        " WHERE I.ItemType = '" & TxtItemType.Tag & "' "
                                TxtItemGroup.AgHelpDataSet(4) = AgL.FillData(mQry, AgL.GCn)
                            End If
                        End If
                    End If

                Case TxtItemCategory.Name
                    If e.KeyCode <> Keys.Enter Then
                        If TxtItemCategory.AgHelpDataSet Is Nothing Then
                            mQry = "SELECT IC.Code, IC.Description, IC.ItemType, IT.Name as ItemTypeName, IC.SalesTaxGroup, IC.Unit, IC.Hsn 
                                    FROM ItemCategory IC 
                                    Left Join ItemType IT On IC.ItemType = IT.Code 
                                    Where IC.ItemType in ('" & AgTemplate.ClsMain.ItemType.FinishedMaterial & "','" & AgTemplate.ClsMain.ItemType.RawMaterial & "','" & AgTemplate.ClsMain.ItemType.Other & "','" & AgTemplate.ClsMain.ItemType.SemiFinishedMaterial & "') Order by IC.Description  "
                            TxtItemCategory.AgHelpDataSet(4) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Sub SetProductName()
        TxtDescription.Text = TxtSpecification.Text + Space(10) + "[" + TxtItemGroup.Text + " | " + TxtItemCategory.Text + "]"
    End Sub


    Private Sub Control_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtItemGroup.Validating, TxtItemCategory.Validating, TxtRate.Validating, TxtSpecification.Validating
        Dim DtTemp As DataTable = Nothing
        Dim DrTemp As DataRow() = Nothing
        Dim i As Integer
        Try
            Select Case sender.NAME
                Case TxtItemCategory.Name
                    If sender.text.ToString.Trim <> "" Then
                        If sender.AgHelpDataSet IsNot Nothing Then
                            DrTemp = sender.AgHelpDataSet.Tables(0).Select("Code = " & AgL.Chk_Text(sender.AgSelectedValue) & "")
                            TxtItemType.Text = AgL.XNull(DrTemp(0)("ItemTypeName"))
                            TxtItemType.Tag = AgL.XNull(DrTemp(0)("ItemType"))
                            TxtUnit.Tag = AgL.XNull(DrTemp(0)("Unit"))
                            TxtUnit.Text = AgL.XNull(DrTemp(0)("Unit"))
                            TxtSalesTaxPostingGroup.Text = AgL.XNull(DrTemp(0)("SalesTaxGroup"))
                            TxtSalesTaxPostingGroup.Tag = AgL.XNull(DrTemp(0)("SalesTaxGroup"))
                            TxtHsn.Text = AgL.XNull(DrTemp(0)("Hsn"))
                        End If
                    Else
                        TxtItemType.Text = ""
                        TxtItemType.Tag = ""
                        TxtUnit.AgSelectedValue = ""
                        TxtSalesTaxPostingGroup.AgSelectedValue = ""
                        TxtHsn.Text = ""
                    End If

                    TxtItemGroup.AgHelpDataSet = Nothing
                    SetProductName()
                Case TxtItemGroup.Name, TxtSpecification.Name
                    SetProductName()
                Case TxtRate.Name
                    If Val(TxtRate.Tag) <> Val(TxtRate.Text) Then
                        For i = 0 To DGLRateType.RowCount - 1
                            If DGLRateType.Item(Col1RateType, i).Value <> "" Then
                                If Val(DGLRateType.Item(Col1Rate, i).Value) = 0 Then
                                    DGLRateType.Item(Col1Rate, i).Value = Val(TxtRate.Text) + (Val(TxtRate.Text) * Val(DGLRateType.Item(Col1Margin, i).Value) / 100)
                                Else
                                    If MsgBox("Do you want to update all rate types", vbYesNo) = vbYes Then
                                        DGLRateType.Item(Col1Rate, i).Value = Val(TxtRate.Text) + (Val(TxtRate.Text) * Val(DGLRateType.Item(Col1Margin, i).Value) / 100)
                                    End If
                                End If
                            End If
                        Next
                    End If
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
        If TxtItemGroup.AgHelpDataSet IsNot Nothing Then TxtItemGroup.AgHelpDataSet = Nothing
        If TxtItemCategory.AgHelpDataSet IsNot Nothing Then TxtItemCategory.AgHelpDataSet = Nothing
    End Sub

    Private Sub FrmItemMaster_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        TxtItemType.Enabled = False

        TxtDescription.Enabled = False
    End Sub

    Private Sub FrmItemMaster_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        Dim DsTemp As DataSet
        TxtCustomFields.Tag = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(ClsMain.Temp_NCat.Item, AgL.GCn)
        AgCustomGrid1.AgCustom = TxtCustomFields.Tag
        IniGrid()

        If TxtManualCode.AgLastValueText = "" Then
            TxtManualCode.Text = AgL.XNull(AgL.Dman_Execute("SELECT  IfNull(Max(CAST(ManualCode AS INTEGER)),0) +1 FROM item  WHERE ABS(ManualCode)>0", AgL.GcnRead).ExecuteScalar)
        Else
            TxtManualCode.Text = (Val(TxtManualCode.AgLastValueText) + 1).ToString
        End If


        TxtItemCategory.Focus()

        TxtUnit.Tag = TxtUnit.AgLastValueTag
        TxtUnit.Text = TxtUnit.AgLastValueText
        TxtRate.Text = TxtRate.AgLastValueText
        TxtItemGroup.Tag = TxtItemGroup.AgLastValueTag
        TxtItemGroup.Text = TxtItemGroup.AgLastValueText
        TxtItemType.Tag = TxtItemType.AgLastValueTag
        TxtItemType.Text = TxtItemType.AgLastValueText
        TxtItemCategory.Tag = TxtItemCategory.AgLastValueTag
        TxtItemCategory.Text = TxtItemCategory.AgLastValueText
        TxtSalesTaxPostingGroup.Tag = TxtSalesTaxPostingGroup.AgLastValueTag
        TxtSalesTaxPostingGroup.Text = TxtSalesTaxPostingGroup.AgLastValueText


        Dim I As Integer
        mQry = " Select  H.Code, H.Description, H.Margin from RateType H Order By H.Sr "
        DsTemp = AgL.FillData(mQry, AgL.GCn)
        With DsTemp.Tables(0)
            DGLRateType.RowCount = 1
            DGLRateType.Rows.Clear()
            If .Rows.Count > 0 Then
                For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                    DGLRateType.Rows.Add()
                    DGLRateType.Item(ColSNo, I).Value = DGLRateType.Rows.Count - 1
                    DGLRateType.Item(Col1RateType, I).Tag = AgL.XNull(.Rows(I)("Code"))
                    DGLRateType.Item(Col1RateType, I).Value = AgL.XNull(.Rows(I)("Description"))
                    DGLRateType.Item(Col1Margin, I).Value = Format(AgL.VNull(.Rows(I)("Margin")), "0.00")
                Next I
            End If
        End With


        ChkIsSystemDefine.Checked = False
        FManageSystemDefine()
    End Sub

    Private Sub FrmItemMaster_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        AgCustomGrid1.Ini_Grid(mSearchCode)
        AgCustomGrid1.SplitGrid = False


        DGLRateType.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(DGLRateType, ColSNo, 40, 5, ColSNo, False, True, False)
            .AddAgTextColumn(DGLRateType, Col1RateType, 120, 0, Col1RateType, True, True, False)
            .AddAgNumberColumn(DGLRateType, Col1Margin, 60, 2, 2, False, Col1Margin, True, True, True)
            .AddAgNumberColumn(DGLRateType, Col1Rate, 90, 8, 2, False, Col1Rate, True, False, True)
        End With
        AgL.AddAgDataGrid(DGLRateType, PnlRateType)
        DGLRateType.EnableHeadersVisualStyles = False
        DGLRateType.AgSkipReadOnlyColumns = True
        DGLRateType.RowHeadersVisible = False
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

        Dim cmd As SQLiteCommand = New SQLiteCommand(sSQL, AgL.GCn)
        Dim Pic As SQLiteParameter = New SQLiteParameter("@pic", SqlDbType.Image)
        Pic.Value = ByteArr
        cmd.Parameters.Add(Pic)
        cmd.ExecuteNonQuery()
    End Sub

    Private Sub FrmItemMaster_BaseEvent_Save_PostTrans(ByVal SearchCode As String) Handles Me.BaseEvent_Save_PostTrans
        Call Update_Picture("Item_Image", "Photo", "Where Code = '" & mSearchCode & "'", Photo_Byte)
    End Sub

    Private Sub FCreateHelpItemGroup()
        mQry = " Select I.Code As Code, I.Description As ItemGroup, I.ItemCategory, I.ItemType, IT.Name AS ItemTypeName, IC.Description AS ItemCategoryDesc " &
                " From ItemGroup I " &
                " LEFT JOIN ItemType IT ON IT.Code = I.ItemType " &
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
            mQry = " SELECT U.*, I.Description AS ItemDesc " &
                    " FROM UnitConversion U " &
                    " LEFT JOIN Item I ON U.Item = I.Code  " &
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
            mQry = " SELECT BD.*, IB.Description AS BaseItemDesc , I.Description AS ItemDesc , P.Description AS ProcessDesc, " &
                    " D1.Description AS Dimension1Desc, D2.Description AS Dimension2Desc, IfNull(V.Cnt,0) AS Cnt " &
                    " FROM BomDetail BD " &
                    " LEFT JOIN Item IB On IB.Code = BD.BaseItem  " &
                    " LEFT JOIN Process P ON P.NCat = BD.Process  " &
                    " LEFT JOIN Dimension1 D1 ON D1.Code = BD.Dimension1  " &
                    " LEFT JOIN Dimension2 D2 ON D2.Code = BD.Dimension2  " &
                    " LEFT JOIN Item I On I.Code = BD.Item  " &
                    " LEFT JOIN ( SELECT L.BaseItem, count(*) AS Cnt  FROM BomDetail L GROUP BY L.BaseItem ) V ON V.BaseItem = BD.Item " &
                    " WHERE BD.BaseItem = '" & SearchCode & "' " &
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

    Private Sub TxtItemCategory_TextChanged(sender As Object, e As EventArgs) Handles TxtItemCategory.TextChanged, TxtItemGroup.TextChanged, TxtSpecification.TextChanged

    End Sub
End Class

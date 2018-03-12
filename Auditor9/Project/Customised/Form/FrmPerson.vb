Imports System.IO
Public Class FrmPerson

    Inherits AgTemplate.TempMaster
    Dim mQry$ = ""
    Protected mGroupNature As String = "", mNature As String = ""

    Dim mMasterType$ = ""

    Dim mSubGroupNature As ESubgroupNature
    Friend WithEvents Pnl1 As Panel
    Dim mIsReturnValue As Boolean = False

    Public Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1Head As String = "Head"
    Public Const Col1Mandatory As String = ""
    Public Const Col1Value As String = "Value"


    Public Const rowContactPerson As Integer = 0
    Public Const rowSalesTaxNo As Integer = 1
    Public Const rowPanNo As Integer = 2
    Public Const rowAadharNo As Integer = 3
    Public Const rowParent As Integer = 4




    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)
    End Sub

    Public Property IsReturnValue() As Boolean
        Get
            IsReturnValue = mIsReturnValue
        End Get
        Set(ByVal value As Boolean)
            mIsReturnValue = value
        End Set
    End Property

    Public Enum ESubgroupNature
        Customer = 0
        Supplier = 1
    End Enum

    Public Class SubGroupConst
        Public Const GroupNature_Debtors As String = "A"
        Public Const Nature_Debtors As String = "Customer"
        Public Const GroupCode_Debtors As String = "0020"
        Public Const GroupNature_Creditors As String = "L"
        Public Const Nature_Creditors As String = "Supplier"
        Public Const GroupCode_Creditors As String = "0016"
    End Class

    Public Property SubGroupNature() As ESubgroupNature
        Get
            SubGroupNature = mSubGroupNature
        End Get
        Set(ByVal value As ESubgroupNature)
            mSubGroupNature = value
        End Set
    End Property

    Public Property MasterType() As String
        Get
            Return mMasterType
        End Get
        Set(ByVal value As String)
            mMasterType = value
        End Set
    End Property

#Region "Designer Code"
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPerson))
        Me.TxtEMail = New AgControls.AgTextBox()
        Me.LblEMail = New System.Windows.Forms.Label()
        Me.TxtMobile = New AgControls.AgTextBox()
        Me.LblMobile = New System.Windows.Forms.Label()
        Me.LblCityReq = New System.Windows.Forms.Label()
        Me.TxtCity = New AgControls.AgTextBox()
        Me.LblCity = New System.Windows.Forms.Label()
        Me.LblAddressReq = New System.Windows.Forms.Label()
        Me.TxtAdd2 = New AgControls.AgTextBox()
        Me.TxtAdd1 = New AgControls.AgTextBox()
        Me.LblAddress = New System.Windows.Forms.Label()
        Me.LblNameReq = New System.Windows.Forms.Label()
        Me.LblManualCodeReq = New System.Windows.Forms.Label()
        Me.TxtManualCode = New AgControls.AgTextBox()
        Me.LblManualCode = New System.Windows.Forms.Label()
        Me.TxtDispName = New AgControls.AgTextBox()
        Me.LblName = New System.Windows.Forms.Label()
        Me.TxtAcGroup = New AgControls.AgTextBox()
        Me.LblAcGroup = New System.Windows.Forms.Label()
        Me.LblAcGroupReq = New System.Windows.Forms.Label()
        Me.TxtCreditDays = New AgControls.AgTextBox()
        Me.LblCreditDays = New System.Windows.Forms.Label()
        Me.TxtCreditLimit = New AgControls.AgTextBox()
        Me.LblCreditLimit = New System.Windows.Forms.Label()
        Me.GrpCreditDetail = New System.Windows.Forms.GroupBox()
        Me.TxtPhone = New AgControls.AgTextBox()
        Me.LblPhone = New System.Windows.Forms.Label()
        Me.TxtSalesTaxGroup = New AgControls.AgTextBox()
        Me.LblSalesTaxGroup = New System.Windows.Forms.Label()
        Me.TxtPinNo = New AgControls.AgTextBox()
        Me.LblPinNo = New System.Windows.Forms.Label()
        Me.TxtPartyType = New AgControls.AgTextBox()
        Me.LblPartyType = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GBoxImportFromExcel = New System.Windows.Forms.GroupBox()
        Me.BtnImprtFromExcel = New System.Windows.Forms.Button()
        Me.Pnl1 = New System.Windows.Forms.Panel()
        Me.GrpUP.SuspendLayout()
        Me.GBoxEntryType.SuspendLayout()
        Me.GBoxMoveToLog.SuspendLayout()
        Me.GBoxApprove.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GBoxDivision.SuspendLayout()
        CType(Me.DTMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpCreditDetail.SuspendLayout()
        Me.GBoxImportFromExcel.SuspendLayout()
        Me.SuspendLayout()
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(1023, 41)
        Me.Topctrl1.tAdd = False
        Me.Topctrl1.tDel = False
        Me.Topctrl1.tEdit = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(0, 338)
        Me.GroupBox1.Size = New System.Drawing.Size(1065, 4)
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(6, 342)
        '
        'TxtEntryBy
        '
        Me.TxtEntryBy.Tag = ""
        Me.TxtEntryBy.Text = ""
        '
        'GBoxEntryType
        '
        Me.GBoxEntryType.Location = New System.Drawing.Point(142, 342)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Tag = ""
        '
        'GBoxMoveToLog
        '
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(556, 342)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Tag = ""
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(400, 342)
        Me.GBoxApprove.Size = New System.Drawing.Size(147, 44)
        Me.GBoxApprove.Text = "Approved By"
        '
        'TxtApproveBy
        '
        Me.TxtApproveBy.Location = New System.Drawing.Point(3, 23)
        Me.TxtApproveBy.Size = New System.Drawing.Size(141, 18)
        Me.TxtApproveBy.Tag = ""
        '
        'CmdDiscard
        '
        Me.CmdDiscard.Location = New System.Drawing.Point(118, 18)
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(702, 342)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(271, 342)
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
        'TxtEMail
        '
        Me.TxtEMail.AgAllowUserToEnableMasterHelp = False
        Me.TxtEMail.AgLastValueTag = Nothing
        Me.TxtEMail.AgLastValueText = Nothing
        Me.TxtEMail.AgMandatory = False
        Me.TxtEMail.AgMasterHelp = False
        Me.TxtEMail.AgNumberLeftPlaces = 0
        Me.TxtEMail.AgNumberNegetiveAllow = False
        Me.TxtEMail.AgNumberRightPlaces = 0
        Me.TxtEMail.AgPickFromLastValue = False
        Me.TxtEMail.AgRowFilter = ""
        Me.TxtEMail.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtEMail.AgSelectedValue = Nothing
        Me.TxtEMail.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtEMail.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtEMail.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtEMail.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtEMail.Location = New System.Drawing.Point(142, 174)
        Me.TxtEMail.MaxLength = 100
        Me.TxtEMail.Name = "TxtEMail"
        Me.TxtEMail.Size = New System.Drawing.Size(292, 18)
        Me.TxtEMail.TabIndex = 10
        '
        'LblEMail
        '
        Me.LblEMail.AutoSize = True
        Me.LblEMail.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblEMail.Location = New System.Drawing.Point(17, 175)
        Me.LblEMail.Name = "LblEMail"
        Me.LblEMail.Size = New System.Drawing.Size(41, 16)
        Me.LblEMail.TabIndex = 799
        Me.LblEMail.Text = "EMail"
        '
        'TxtMobile
        '
        Me.TxtMobile.AgAllowUserToEnableMasterHelp = False
        Me.TxtMobile.AgLastValueTag = Nothing
        Me.TxtMobile.AgLastValueText = Nothing
        Me.TxtMobile.AgMandatory = False
        Me.TxtMobile.AgMasterHelp = False
        Me.TxtMobile.AgNumberLeftPlaces = 0
        Me.TxtMobile.AgNumberNegetiveAllow = False
        Me.TxtMobile.AgNumberRightPlaces = 0
        Me.TxtMobile.AgPickFromLastValue = False
        Me.TxtMobile.AgRowFilter = ""
        Me.TxtMobile.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtMobile.AgSelectedValue = Nothing
        Me.TxtMobile.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtMobile.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtMobile.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtMobile.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMobile.Location = New System.Drawing.Point(321, 154)
        Me.TxtMobile.MaxLength = 35
        Me.TxtMobile.Name = "TxtMobile"
        Me.TxtMobile.Size = New System.Drawing.Size(113, 18)
        Me.TxtMobile.TabIndex = 9
        '
        'LblMobile
        '
        Me.LblMobile.AutoSize = True
        Me.LblMobile.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMobile.Location = New System.Drawing.Point(262, 155)
        Me.LblMobile.Name = "LblMobile"
        Me.LblMobile.Size = New System.Drawing.Size(46, 16)
        Me.LblMobile.TabIndex = 793
        Me.LblMobile.Text = "Mobile"
        '
        'LblCityReq
        '
        Me.LblCityReq.AutoSize = True
        Me.LblCityReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblCityReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblCityReq.Location = New System.Drawing.Point(125, 141)
        Me.LblCityReq.Name = "LblCityReq"
        Me.LblCityReq.Size = New System.Drawing.Size(10, 7)
        Me.LblCityReq.TabIndex = 791
        Me.LblCityReq.Text = "Ä"
        '
        'TxtCity
        '
        Me.TxtCity.AgAllowUserToEnableMasterHelp = False
        Me.TxtCity.AgLastValueTag = Nothing
        Me.TxtCity.AgLastValueText = Nothing
        Me.TxtCity.AgMandatory = True
        Me.TxtCity.AgMasterHelp = False
        Me.TxtCity.AgNumberLeftPlaces = 0
        Me.TxtCity.AgNumberNegetiveAllow = False
        Me.TxtCity.AgNumberRightPlaces = 0
        Me.TxtCity.AgPickFromLastValue = False
        Me.TxtCity.AgRowFilter = ""
        Me.TxtCity.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCity.AgSelectedValue = Nothing
        Me.TxtCity.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCity.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtCity.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtCity.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCity.Location = New System.Drawing.Point(142, 134)
        Me.TxtCity.MaxLength = 0
        Me.TxtCity.Name = "TxtCity"
        Me.TxtCity.Size = New System.Drawing.Size(115, 18)
        Me.TxtCity.TabIndex = 6
        '
        'LblCity
        '
        Me.LblCity.AutoSize = True
        Me.LblCity.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCity.Location = New System.Drawing.Point(17, 134)
        Me.LblCity.Name = "LblCity"
        Me.LblCity.Size = New System.Drawing.Size(31, 16)
        Me.LblCity.TabIndex = 790
        Me.LblCity.Text = "City"
        '
        'LblAddressReq
        '
        Me.LblAddressReq.AutoSize = True
        Me.LblAddressReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblAddressReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblAddressReq.Location = New System.Drawing.Point(125, 102)
        Me.LblAddressReq.Name = "LblAddressReq"
        Me.LblAddressReq.Size = New System.Drawing.Size(10, 7)
        Me.LblAddressReq.TabIndex = 785
        Me.LblAddressReq.Text = "Ä"
        '
        'TxtAdd2
        '
        Me.TxtAdd2.AgAllowUserToEnableMasterHelp = False
        Me.TxtAdd2.AgLastValueTag = Nothing
        Me.TxtAdd2.AgLastValueText = Nothing
        Me.TxtAdd2.AgMandatory = False
        Me.TxtAdd2.AgMasterHelp = True
        Me.TxtAdd2.AgNumberLeftPlaces = 8
        Me.TxtAdd2.AgNumberNegetiveAllow = False
        Me.TxtAdd2.AgNumberRightPlaces = 2
        Me.TxtAdd2.AgPickFromLastValue = False
        Me.TxtAdd2.AgRowFilter = ""
        Me.TxtAdd2.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtAdd2.AgSelectedValue = Nothing
        Me.TxtAdd2.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtAdd2.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtAdd2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtAdd2.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAdd2.Location = New System.Drawing.Point(142, 114)
        Me.TxtAdd2.MaxLength = 50
        Me.TxtAdd2.Name = "TxtAdd2"
        Me.TxtAdd2.Size = New System.Drawing.Size(292, 18)
        Me.TxtAdd2.TabIndex = 5
        '
        'TxtAdd1
        '
        Me.TxtAdd1.AgAllowUserToEnableMasterHelp = False
        Me.TxtAdd1.AgLastValueTag = Nothing
        Me.TxtAdd1.AgLastValueText = Nothing
        Me.TxtAdd1.AgMandatory = True
        Me.TxtAdd1.AgMasterHelp = True
        Me.TxtAdd1.AgNumberLeftPlaces = 8
        Me.TxtAdd1.AgNumberNegetiveAllow = False
        Me.TxtAdd1.AgNumberRightPlaces = 2
        Me.TxtAdd1.AgPickFromLastValue = False
        Me.TxtAdd1.AgRowFilter = ""
        Me.TxtAdd1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtAdd1.AgSelectedValue = Nothing
        Me.TxtAdd1.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtAdd1.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtAdd1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtAdd1.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAdd1.Location = New System.Drawing.Point(142, 94)
        Me.TxtAdd1.MaxLength = 50
        Me.TxtAdd1.Name = "TxtAdd1"
        Me.TxtAdd1.Size = New System.Drawing.Size(292, 18)
        Me.TxtAdd1.TabIndex = 4
        '
        'LblAddress
        '
        Me.LblAddress.AutoSize = True
        Me.LblAddress.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblAddress.Location = New System.Drawing.Point(17, 94)
        Me.LblAddress.Name = "LblAddress"
        Me.LblAddress.Size = New System.Drawing.Size(56, 16)
        Me.LblAddress.TabIndex = 784
        Me.LblAddress.Text = "Address"
        '
        'LblNameReq
        '
        Me.LblNameReq.AutoSize = True
        Me.LblNameReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblNameReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblNameReq.Location = New System.Drawing.Point(125, 81)
        Me.LblNameReq.Name = "LblNameReq"
        Me.LblNameReq.Size = New System.Drawing.Size(10, 7)
        Me.LblNameReq.TabIndex = 781
        Me.LblNameReq.Text = "Ä"
        '
        'LblManualCodeReq
        '
        Me.LblManualCodeReq.AutoSize = True
        Me.LblManualCodeReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblManualCodeReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblManualCodeReq.Location = New System.Drawing.Point(305, 61)
        Me.LblManualCodeReq.Name = "LblManualCodeReq"
        Me.LblManualCodeReq.Size = New System.Drawing.Size(10, 7)
        Me.LblManualCodeReq.TabIndex = 778
        Me.LblManualCodeReq.Text = "Ä"
        '
        'TxtManualCode
        '
        Me.TxtManualCode.AgAllowUserToEnableMasterHelp = False
        Me.TxtManualCode.AgLastValueTag = Nothing
        Me.TxtManualCode.AgLastValueText = Nothing
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
        Me.TxtManualCode.Location = New System.Drawing.Point(321, 54)
        Me.TxtManualCode.MaxLength = 20
        Me.TxtManualCode.Name = "TxtManualCode"
        Me.TxtManualCode.Size = New System.Drawing.Size(113, 18)
        Me.TxtManualCode.TabIndex = 2
        '
        'LblManualCode
        '
        Me.LblManualCode.AutoSize = True
        Me.LblManualCode.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblManualCode.Location = New System.Drawing.Point(268, 55)
        Me.LblManualCode.Name = "LblManualCode"
        Me.LblManualCode.Size = New System.Drawing.Size(38, 16)
        Me.LblManualCode.TabIndex = 775
        Me.LblManualCode.Text = "Code"
        '
        'TxtDispName
        '
        Me.TxtDispName.AgAllowUserToEnableMasterHelp = False
        Me.TxtDispName.AgLastValueTag = Nothing
        Me.TxtDispName.AgLastValueText = Nothing
        Me.TxtDispName.AgMandatory = True
        Me.TxtDispName.AgMasterHelp = True
        Me.TxtDispName.AgNumberLeftPlaces = 0
        Me.TxtDispName.AgNumberNegetiveAllow = False
        Me.TxtDispName.AgNumberRightPlaces = 0
        Me.TxtDispName.AgPickFromLastValue = False
        Me.TxtDispName.AgRowFilter = ""
        Me.TxtDispName.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtDispName.AgSelectedValue = Nothing
        Me.TxtDispName.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtDispName.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtDispName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtDispName.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDispName.Location = New System.Drawing.Point(142, 74)
        Me.TxtDispName.MaxLength = 100
        Me.TxtDispName.Name = "TxtDispName"
        Me.TxtDispName.Size = New System.Drawing.Size(292, 18)
        Me.TxtDispName.TabIndex = 3
        '
        'LblName
        '
        Me.LblName.AutoSize = True
        Me.LblName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblName.Location = New System.Drawing.Point(17, 74)
        Me.LblName.Name = "LblName"
        Me.LblName.Size = New System.Drawing.Size(42, 16)
        Me.LblName.TabIndex = 777
        Me.LblName.Text = "Name"
        '
        'TxtAcGroup
        '
        Me.TxtAcGroup.AgAllowUserToEnableMasterHelp = False
        Me.TxtAcGroup.AgLastValueTag = Nothing
        Me.TxtAcGroup.AgLastValueText = Nothing
        Me.TxtAcGroup.AgMandatory = False
        Me.TxtAcGroup.AgMasterHelp = False
        Me.TxtAcGroup.AgNumberLeftPlaces = 0
        Me.TxtAcGroup.AgNumberNegetiveAllow = False
        Me.TxtAcGroup.AgNumberRightPlaces = 0
        Me.TxtAcGroup.AgPickFromLastValue = False
        Me.TxtAcGroup.AgRowFilter = ""
        Me.TxtAcGroup.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtAcGroup.AgSelectedValue = Nothing
        Me.TxtAcGroup.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtAcGroup.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtAcGroup.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtAcGroup.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAcGroup.Location = New System.Drawing.Point(142, 194)
        Me.TxtAcGroup.MaxLength = 100
        Me.TxtAcGroup.Name = "TxtAcGroup"
        Me.TxtAcGroup.Size = New System.Drawing.Size(292, 18)
        Me.TxtAcGroup.TabIndex = 11
        '
        'LblAcGroup
        '
        Me.LblAcGroup.AutoSize = True
        Me.LblAcGroup.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblAcGroup.Location = New System.Drawing.Point(17, 194)
        Me.LblAcGroup.Name = "LblAcGroup"
        Me.LblAcGroup.Size = New System.Drawing.Size(67, 16)
        Me.LblAcGroup.TabIndex = 860
        Me.LblAcGroup.Text = "A/c Group"
        '
        'LblAcGroupReq
        '
        Me.LblAcGroupReq.AutoSize = True
        Me.LblAcGroupReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblAcGroupReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblAcGroupReq.Location = New System.Drawing.Point(123, 200)
        Me.LblAcGroupReq.Name = "LblAcGroupReq"
        Me.LblAcGroupReq.Size = New System.Drawing.Size(10, 7)
        Me.LblAcGroupReq.TabIndex = 861
        Me.LblAcGroupReq.Text = "Ä"
        '
        'TxtCreditDays
        '
        Me.TxtCreditDays.AgAllowUserToEnableMasterHelp = False
        Me.TxtCreditDays.AgLastValueTag = Nothing
        Me.TxtCreditDays.AgLastValueText = Nothing
        Me.TxtCreditDays.AgMandatory = False
        Me.TxtCreditDays.AgMasterHelp = False
        Me.TxtCreditDays.AgNumberLeftPlaces = 0
        Me.TxtCreditDays.AgNumberNegetiveAllow = False
        Me.TxtCreditDays.AgNumberRightPlaces = 0
        Me.TxtCreditDays.AgPickFromLastValue = False
        Me.TxtCreditDays.AgRowFilter = ""
        Me.TxtCreditDays.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCreditDays.AgSelectedValue = Nothing
        Me.TxtCreditDays.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCreditDays.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtCreditDays.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtCreditDays.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCreditDays.Location = New System.Drawing.Point(114, 19)
        Me.TxtCreditDays.MaxLength = 5
        Me.TxtCreditDays.Name = "TxtCreditDays"
        Me.TxtCreditDays.Size = New System.Drawing.Size(77, 18)
        Me.TxtCreditDays.TabIndex = 13
        '
        'LblCreditDays
        '
        Me.LblCreditDays.AutoSize = True
        Me.LblCreditDays.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCreditDays.Location = New System.Drawing.Point(19, 20)
        Me.LblCreditDays.Name = "LblCreditDays"
        Me.LblCreditDays.Size = New System.Drawing.Size(76, 16)
        Me.LblCreditDays.TabIndex = 863
        Me.LblCreditDays.Text = "Credit Days"
        '
        'TxtCreditLimit
        '
        Me.TxtCreditLimit.AgAllowUserToEnableMasterHelp = False
        Me.TxtCreditLimit.AgLastValueTag = Nothing
        Me.TxtCreditLimit.AgLastValueText = Nothing
        Me.TxtCreditLimit.AgMandatory = False
        Me.TxtCreditLimit.AgMasterHelp = False
        Me.TxtCreditLimit.AgNumberLeftPlaces = 0
        Me.TxtCreditLimit.AgNumberNegetiveAllow = False
        Me.TxtCreditLimit.AgNumberRightPlaces = 0
        Me.TxtCreditLimit.AgPickFromLastValue = False
        Me.TxtCreditLimit.AgRowFilter = ""
        Me.TxtCreditLimit.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCreditLimit.AgSelectedValue = Nothing
        Me.TxtCreditLimit.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCreditLimit.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtCreditLimit.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtCreditLimit.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCreditLimit.Location = New System.Drawing.Point(313, 18)
        Me.TxtCreditLimit.MaxLength = 10
        Me.TxtCreditLimit.Name = "TxtCreditLimit"
        Me.TxtCreditLimit.Size = New System.Drawing.Size(77, 18)
        Me.TxtCreditLimit.TabIndex = 14
        '
        'LblCreditLimit
        '
        Me.LblCreditLimit.AutoSize = True
        Me.LblCreditLimit.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCreditLimit.Location = New System.Drawing.Point(230, 19)
        Me.LblCreditLimit.Name = "LblCreditLimit"
        Me.LblCreditLimit.Size = New System.Drawing.Size(74, 16)
        Me.LblCreditLimit.TabIndex = 865
        Me.LblCreditLimit.Text = "Credit Limit"
        '
        'GrpCreditDetail
        '
        Me.GrpCreditDetail.AutoSize = True
        Me.GrpCreditDetail.BackColor = System.Drawing.Color.Transparent
        Me.GrpCreditDetail.Controls.Add(Me.TxtCreditLimit)
        Me.GrpCreditDetail.Controls.Add(Me.LblCreditDays)
        Me.GrpCreditDetail.Controls.Add(Me.LblCreditLimit)
        Me.GrpCreditDetail.Controls.Add(Me.TxtCreditDays)
        Me.GrpCreditDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GrpCreditDetail.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GrpCreditDetail.Location = New System.Drawing.Point(19, 249)
        Me.GrpCreditDetail.Name = "GrpCreditDetail"
        Me.GrpCreditDetail.Size = New System.Drawing.Size(413, 56)
        Me.GrpCreditDetail.TabIndex = 22
        Me.GrpCreditDetail.TabStop = False
        Me.GrpCreditDetail.Text = "Credit Detail"
        '
        'TxtPhone
        '
        Me.TxtPhone.AgAllowUserToEnableMasterHelp = False
        Me.TxtPhone.AgLastValueTag = Nothing
        Me.TxtPhone.AgLastValueText = Nothing
        Me.TxtPhone.AgMandatory = False
        Me.TxtPhone.AgMasterHelp = False
        Me.TxtPhone.AgNumberLeftPlaces = 0
        Me.TxtPhone.AgNumberNegetiveAllow = False
        Me.TxtPhone.AgNumberRightPlaces = 0
        Me.TxtPhone.AgPickFromLastValue = False
        Me.TxtPhone.AgRowFilter = ""
        Me.TxtPhone.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtPhone.AgSelectedValue = Nothing
        Me.TxtPhone.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPhone.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtPhone.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPhone.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPhone.Location = New System.Drawing.Point(142, 154)
        Me.TxtPhone.MaxLength = 35
        Me.TxtPhone.Name = "TxtPhone"
        Me.TxtPhone.Size = New System.Drawing.Size(115, 18)
        Me.TxtPhone.TabIndex = 8
        '
        'LblPhone
        '
        Me.LblPhone.AutoSize = True
        Me.LblPhone.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPhone.Location = New System.Drawing.Point(17, 155)
        Me.LblPhone.Name = "LblPhone"
        Me.LblPhone.Size = New System.Drawing.Size(77, 16)
        Me.LblPhone.TabIndex = 864
        Me.LblPhone.Text = "Contact No."
        '
        'TxtSalesTaxGroup
        '
        Me.TxtSalesTaxGroup.AgAllowUserToEnableMasterHelp = False
        Me.TxtSalesTaxGroup.AgLastValueTag = Nothing
        Me.TxtSalesTaxGroup.AgLastValueText = Nothing
        Me.TxtSalesTaxGroup.AgMandatory = False
        Me.TxtSalesTaxGroup.AgMasterHelp = False
        Me.TxtSalesTaxGroup.AgNumberLeftPlaces = 0
        Me.TxtSalesTaxGroup.AgNumberNegetiveAllow = False
        Me.TxtSalesTaxGroup.AgNumberRightPlaces = 0
        Me.TxtSalesTaxGroup.AgPickFromLastValue = False
        Me.TxtSalesTaxGroup.AgRowFilter = ""
        Me.TxtSalesTaxGroup.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtSalesTaxGroup.AgSelectedValue = Nothing
        Me.TxtSalesTaxGroup.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtSalesTaxGroup.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtSalesTaxGroup.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtSalesTaxGroup.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSalesTaxGroup.Location = New System.Drawing.Point(142, 214)
        Me.TxtSalesTaxGroup.MaxLength = 20
        Me.TxtSalesTaxGroup.Name = "TxtSalesTaxGroup"
        Me.TxtSalesTaxGroup.Size = New System.Drawing.Size(292, 18)
        Me.TxtSalesTaxGroup.TabIndex = 12
        '
        'LblSalesTaxGroup
        '
        Me.LblSalesTaxGroup.AutoSize = True
        Me.LblSalesTaxGroup.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblSalesTaxGroup.Location = New System.Drawing.Point(17, 215)
        Me.LblSalesTaxGroup.Name = "LblSalesTaxGroup"
        Me.LblSalesTaxGroup.Size = New System.Drawing.Size(104, 16)
        Me.LblSalesTaxGroup.TabIndex = 867
        Me.LblSalesTaxGroup.Text = "Sales Tax Group"
        '
        'TxtPinNo
        '
        Me.TxtPinNo.AgAllowUserToEnableMasterHelp = False
        Me.TxtPinNo.AgLastValueTag = Nothing
        Me.TxtPinNo.AgLastValueText = Nothing
        Me.TxtPinNo.AgMandatory = False
        Me.TxtPinNo.AgMasterHelp = False
        Me.TxtPinNo.AgNumberLeftPlaces = 0
        Me.TxtPinNo.AgNumberNegetiveAllow = False
        Me.TxtPinNo.AgNumberRightPlaces = 0
        Me.TxtPinNo.AgPickFromLastValue = False
        Me.TxtPinNo.AgRowFilter = ""
        Me.TxtPinNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtPinNo.AgSelectedValue = Nothing
        Me.TxtPinNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPinNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtPinNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPinNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPinNo.Location = New System.Drawing.Point(321, 134)
        Me.TxtPinNo.MaxLength = 6
        Me.TxtPinNo.Name = "TxtPinNo"
        Me.TxtPinNo.Size = New System.Drawing.Size(113, 18)
        Me.TxtPinNo.TabIndex = 7
        '
        'LblPinNo
        '
        Me.LblPinNo.AutoSize = True
        Me.LblPinNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPinNo.Location = New System.Drawing.Point(262, 135)
        Me.LblPinNo.Name = "LblPinNo"
        Me.LblPinNo.Size = New System.Drawing.Size(53, 16)
        Me.LblPinNo.TabIndex = 885
        Me.LblPinNo.Text = "PIN No."
        '
        'TxtPartyType
        '
        Me.TxtPartyType.AgAllowUserToEnableMasterHelp = False
        Me.TxtPartyType.AgLastValueTag = Nothing
        Me.TxtPartyType.AgLastValueText = Nothing
        Me.TxtPartyType.AgMandatory = False
        Me.TxtPartyType.AgMasterHelp = False
        Me.TxtPartyType.AgNumberLeftPlaces = 0
        Me.TxtPartyType.AgNumberNegetiveAllow = False
        Me.TxtPartyType.AgNumberRightPlaces = 0
        Me.TxtPartyType.AgPickFromLastValue = False
        Me.TxtPartyType.AgRowFilter = ""
        Me.TxtPartyType.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtPartyType.AgSelectedValue = Nothing
        Me.TxtPartyType.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPartyType.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtPartyType.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPartyType.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPartyType.Location = New System.Drawing.Point(141, 54)
        Me.TxtPartyType.MaxLength = 50
        Me.TxtPartyType.Name = "TxtPartyType"
        Me.TxtPartyType.Size = New System.Drawing.Size(110, 18)
        Me.TxtPartyType.TabIndex = 1
        '
        'LblPartyType
        '
        Me.LblPartyType.AutoSize = True
        Me.LblPartyType.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPartyType.Location = New System.Drawing.Point(18, 54)
        Me.LblPartyType.Name = "LblPartyType"
        Me.LblPartyType.Size = New System.Drawing.Size(70, 16)
        Me.LblPartyType.TabIndex = 887
        Me.LblPartyType.Text = "Party Type"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(125, 220)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(10, 7)
        Me.Label2.TabIndex = 890
        Me.Label2.Text = "Ä"
        '
        'GBoxImportFromExcel
        '
        Me.GBoxImportFromExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GBoxImportFromExcel.BackColor = System.Drawing.Color.Transparent
        Me.GBoxImportFromExcel.Controls.Add(Me.BtnImprtFromExcel)
        Me.GBoxImportFromExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GBoxImportFromExcel.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBoxImportFromExcel.ForeColor = System.Drawing.Color.Maroon
        Me.GBoxImportFromExcel.Location = New System.Drawing.Point(873, 47)
        Me.GBoxImportFromExcel.Name = "GBoxImportFromExcel"
        Me.GBoxImportFromExcel.Size = New System.Drawing.Size(99, 47)
        Me.GBoxImportFromExcel.TabIndex = 1015
        Me.GBoxImportFromExcel.TabStop = False
        Me.GBoxImportFromExcel.Tag = "UP"
        Me.GBoxImportFromExcel.Text = "Import From Excel"
        Me.GBoxImportFromExcel.Visible = False
        '
        'BtnImprtFromExcel
        '
        Me.BtnImprtFromExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnImprtFromExcel.Image = CType(resources.GetObject("BtnImprtFromExcel.Image"), System.Drawing.Image)
        Me.BtnImprtFromExcel.Location = New System.Drawing.Point(58, 9)
        Me.BtnImprtFromExcel.Name = "BtnImprtFromExcel"
        Me.BtnImprtFromExcel.Size = New System.Drawing.Size(36, 34)
        Me.BtnImprtFromExcel.TabIndex = 669
        Me.BtnImprtFromExcel.TabStop = False
        Me.BtnImprtFromExcel.UseVisualStyleBackColor = True
        '
        'Pnl1
        '
        Me.Pnl1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Pnl1.Location = New System.Drawing.Point(459, 54)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(408, 251)
        Me.Pnl1.TabIndex = 15
        '
        'FrmPerson
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(1023, 386)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.GBoxImportFromExcel)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtPartyType)
        Me.Controls.Add(Me.LblPartyType)
        Me.Controls.Add(Me.TxtPinNo)
        Me.Controls.Add(Me.LblPinNo)
        Me.Controls.Add(Me.TxtSalesTaxGroup)
        Me.Controls.Add(Me.LblSalesTaxGroup)
        Me.Controls.Add(Me.TxtPhone)
        Me.Controls.Add(Me.LblPhone)
        Me.Controls.Add(Me.GrpCreditDetail)
        Me.Controls.Add(Me.LblAcGroupReq)
        Me.Controls.Add(Me.TxtAcGroup)
        Me.Controls.Add(Me.LblAcGroup)
        Me.Controls.Add(Me.TxtEMail)
        Me.Controls.Add(Me.LblEMail)
        Me.Controls.Add(Me.TxtMobile)
        Me.Controls.Add(Me.LblMobile)
        Me.Controls.Add(Me.LblCityReq)
        Me.Controls.Add(Me.TxtCity)
        Me.Controls.Add(Me.LblCity)
        Me.Controls.Add(Me.LblAddressReq)
        Me.Controls.Add(Me.TxtAdd2)
        Me.Controls.Add(Me.TxtAdd1)
        Me.Controls.Add(Me.LblAddress)
        Me.Controls.Add(Me.LblNameReq)
        Me.Controls.Add(Me.LblManualCodeReq)
        Me.Controls.Add(Me.TxtManualCode)
        Me.Controls.Add(Me.LblManualCode)
        Me.Controls.Add(Me.TxtDispName)
        Me.Controls.Add(Me.LblName)
        Me.Name = "FrmPerson"
        Me.Text = "Buyer Master"
        Me.Controls.SetChildIndex(Me.LblName, 0)
        Me.Controls.SetChildIndex(Me.TxtDispName, 0)
        Me.Controls.SetChildIndex(Me.LblManualCode, 0)
        Me.Controls.SetChildIndex(Me.TxtManualCode, 0)
        Me.Controls.SetChildIndex(Me.LblManualCodeReq, 0)
        Me.Controls.SetChildIndex(Me.LblNameReq, 0)
        Me.Controls.SetChildIndex(Me.LblAddress, 0)
        Me.Controls.SetChildIndex(Me.TxtAdd1, 0)
        Me.Controls.SetChildIndex(Me.TxtAdd2, 0)
        Me.Controls.SetChildIndex(Me.LblAddressReq, 0)
        Me.Controls.SetChildIndex(Me.LblCity, 0)
        Me.Controls.SetChildIndex(Me.TxtCity, 0)
        Me.Controls.SetChildIndex(Me.LblCityReq, 0)
        Me.Controls.SetChildIndex(Me.LblMobile, 0)
        Me.Controls.SetChildIndex(Me.TxtMobile, 0)
        Me.Controls.SetChildIndex(Me.LblEMail, 0)
        Me.Controls.SetChildIndex(Me.TxtEMail, 0)
        Me.Controls.SetChildIndex(Me.LblAcGroup, 0)
        Me.Controls.SetChildIndex(Me.TxtAcGroup, 0)
        Me.Controls.SetChildIndex(Me.LblAcGroupReq, 0)
        Me.Controls.SetChildIndex(Me.GrpCreditDetail, 0)
        Me.Controls.SetChildIndex(Me.LblPhone, 0)
        Me.Controls.SetChildIndex(Me.TxtPhone, 0)
        Me.Controls.SetChildIndex(Me.LblSalesTaxGroup, 0)
        Me.Controls.SetChildIndex(Me.TxtSalesTaxGroup, 0)
        Me.Controls.SetChildIndex(Me.LblPinNo, 0)
        Me.Controls.SetChildIndex(Me.TxtPinNo, 0)
        Me.Controls.SetChildIndex(Me.LblPartyType, 0)
        Me.Controls.SetChildIndex(Me.TxtPartyType, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.GBoxImportFromExcel, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
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
        Me.GrpCreditDetail.ResumeLayout(False)
        Me.GrpCreditDetail.PerformLayout()
        Me.GBoxImportFromExcel.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Protected WithEvents LblName As System.Windows.Forms.Label
    Public WithEvents TxtDispName As AgControls.AgTextBox
    Protected WithEvents LblManualCode As System.Windows.Forms.Label
    Protected WithEvents TxtManualCode As AgControls.AgTextBox
    Protected WithEvents LblManualCodeReq As System.Windows.Forms.Label
    Protected WithEvents LblNameReq As System.Windows.Forms.Label
    Protected WithEvents LblAddress As System.Windows.Forms.Label
    Protected WithEvents TxtAdd1 As AgControls.AgTextBox
    Protected WithEvents TxtAdd2 As AgControls.AgTextBox
    Protected WithEvents LblAddressReq As System.Windows.Forms.Label
    Protected WithEvents LblCity As System.Windows.Forms.Label
    Protected WithEvents TxtCity As AgControls.AgTextBox
    Protected WithEvents LblCityReq As System.Windows.Forms.Label
    Protected WithEvents LblMobile As System.Windows.Forms.Label
    Protected WithEvents TxtMobile As AgControls.AgTextBox
    Protected WithEvents LblEMail As System.Windows.Forms.Label
    Protected WithEvents TxtEMail As AgControls.AgTextBox
    Protected WithEvents TxtAcGroup As AgControls.AgTextBox
    Protected WithEvents LblAcGroup As System.Windows.Forms.Label
    Protected WithEvents TxtCreditDays As AgControls.AgTextBox
    Protected WithEvents LblCreditDays As System.Windows.Forms.Label
    Protected WithEvents TxtCreditLimit As AgControls.AgTextBox
    Protected WithEvents LblCreditLimit As System.Windows.Forms.Label
    Protected WithEvents GrpCreditDetail As System.Windows.Forms.GroupBox
    Protected WithEvents TxtPhone As AgControls.AgTextBox
    Protected WithEvents LblPhone As System.Windows.Forms.Label
    Protected WithEvents TxtSalesTaxGroup As AgControls.AgTextBox
    Protected WithEvents LblSalesTaxGroup As System.Windows.Forms.Label
    Protected WithEvents TxtPinNo As AgControls.AgTextBox
    Protected WithEvents LblPinNo As System.Windows.Forms.Label
    Protected WithEvents TxtPartyType As AgControls.AgTextBox
    Protected WithEvents LblPartyType As System.Windows.Forms.Label
    Protected WithEvents LblAcGroupReq As System.Windows.Forms.Label
    Protected WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents GBoxImportFromExcel As System.Windows.Forms.GroupBox
    Public WithEvents BtnImprtFromExcel As System.Windows.Forms.Button
#End Region

    Private Sub FrmShade_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        AgL.PubFindQry = " SELECT H.SubCode AS SearchCode,  H.DispName AS [Display Name], " &
                        " H.ManualCode AS [Manual Code], H.Add1, H.Add2, H.Add3, C.CityName AS [City Name], " &
                        " H.Mobile, H.EMail, " &
                        " H.EntryBy AS [Entry By], H.EntryDate AS [Entry Date], H.EntryType AS [Entry Type], " &
                        " H.Status, AG.GroupName AS [GROUP No], D.Div_Name AS Division,SM.Name AS [Site Name] " &
                        " FROM SubGroup H " &
                        " LEFT JOIN Division D ON D.Div_Code=H.Div_Code  " &
                        " LEFT JOIN SiteMast SM ON SM.Code=H.Site_Code " &
                        " LEFT JOIN AcGroup AG ON AG.GroupCode = H.GroupCode " &
                        " LEFT JOIN City C ON C.CityCode = H.CityCode  " &
                        " WHERE MasterType = '" & mMasterType & "' AND H.Site_Code =" & AgL.Chk_Text(AgL.PubSiteCode) & " "
        AgL.PubFindQryOrdBy = "[Name]"
    End Sub

    Private Sub FrmShade_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "SubGroup"
        LogTableName = "SubGroup_Log"

        PrimaryField = "SubCode"
    End Sub

    Private Sub ApplySubgroupTypeSetting(SubgroupType As String)
        Dim mQry As String
        Dim DsTemp As DataSet
        Try
            mQry = "Select S.*, A.GroupName as AcGroupName, A.GroupNature, A.Nature 
                    from subgroupTypeSetting S
                    Left Join AcGroup A On S.AcGroupCode = A.GroupCode
                   Where SubgroupType = '" & SubgroupType & "' "
            DsTemp = AgL.FillData(mQry, AgL.GCn)
            With DsTemp.Tables(0)
                If DsTemp.Tables(0).Rows.Count > 0 Then
                    Dgl1.Rows(rowContactPerson).Visible = CBool(AgL.XNull(.Rows(0)("IsVisible_ContactPerson")))
                    Dgl1.Rows(rowPanNo).Visible = CBool(AgL.XNull(.Rows(0)("IsVisible_PanNo")))
                    Dgl1.Rows(rowAadharNo).Visible = CBool(AgL.XNull(.Rows(0)("IsVisible_AadharNo")))
                    Dgl1.Rows(rowSalesTaxNo).Visible = CBool(AgL.XNull(.Rows(0)("IsVisible_SalesTaxNo")))
                    Dgl1.Rows(rowParent).Visible = CBool(AgL.XNull(.Rows(0)("IsVisible_Parent")))
                    If AgL.XNull(.Rows(0)("Caption_Parent")) <> "" Then
                        Dgl1.Item(Col1Head, rowParent).Value = AgL.XNull(.Rows(0)("Caption_Parent"))
                    End If
                    TxtAcGroup.Tag = AgL.XNull(.Rows(0)("AcGroupCode"))
                    TxtAcGroup.Text = AgL.XNull(.Rows(0)("AcGroupName"))
                    mGroupNature = AgL.XNull(.Rows(0)("GroupNature"))
                    mNature = AgL.XNull(.Rows(0)("Nature"))
                    GrpCreditDetail.Visible = CBool(AgL.XNull(.Rows(0)("IsVisible_CreditLimit")))

                End If

            End With
        Catch ex As Exception
            MsgBox(ex.Message & " [ApplySubgroupTypeSetting]")
        End Try
    End Sub


    Private Sub FrmQuality1_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList
        mQry = "Select S.SubCode as Code, S.ManualCode, S.DispName as Name " &
                " From SubGroup S  " &
                " Where S.Site_Code =" & AgL.Chk_Text(AgL.PubSiteCode) & "" &
                " Order By S.ManualCode "
        TxtManualCode.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)

        mQry = "Select S.SubCode as Code, S.DispName As Name " &
                " From SubGroup S " &
                " Where S.Site_Code =" & AgL.Chk_Text(AgL.PubSiteCode) & "" &
                " Order By S.DispName "
        TxtDispName.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)

        mQry = "SELECT C.CityCode AS Code, C.CityName, C.State " &
                " FROM City C  "
        TxtCity.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

        mQry = "SELECT A.GroupCode AS Code, A.GroupName AS Name, A.GroupNature , A.Nature  " &
                  " FROM AcGroup A "
        TxtAcGroup.AgHelpDataSet(2) = AgL.FillData(mQry, AgL.GCn)

        mQry = " SELECT H.SubgroupType AS Code, SubgroupType as Name FROM SubGroupType H  "
        TxtPartyType.AgHelpDataSet(0) = AgL.FillData(mQry, AgL.GCn)

        mQry = " SELECT Description AS Code, Description  FROM PostingGroupSalesTaxParty "
        TxtSalesTaxGroup.AgHelpDataSet(0) = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub FrmShade_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        mQry = "Select S.SubCode As SearchCode " &
            " From SubGroup S  " &
            " WHERE IfNull(S.IsDeleted,0)=0  And MasterType = '" & mMasterType & "' AND Site_Code =" & AgL.Chk_Text(AgL.PubSiteCode) & " "

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmQuality1_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DsTemp As DataSet
        Dim DrTemp As DataRow() = Nothing

        mQry = "Select S.*, P.Name as ParentName 
                     From SubGroup S 
                    Left Join viewHelpSubgroup P on S.Parent = P.Code
                     Where S.SubCode='" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                TxtPartyType.Tag = AgL.XNull(.Rows(0)("SubgroupType"))
                TxtPartyType.Text = AgL.XNull(.Rows(0)("SubgroupType"))
                ApplySubgroupTypeSetting(TxtPartyType.Text)
                mInternalCode = AgL.XNull(.Rows(0)("SubCode"))
                TxtManualCode.Text = AgL.XNull(.Rows(0)("ManualCode"))
                TxtDispName.Text = AgL.XNull(.Rows(0)("DispName"))
                TxtAcGroup.AgSelectedValue = AgL.XNull(.Rows(0)("GroupCode"))
                TxtAdd1.Text = AgL.XNull(.Rows(0)("Add1"))
                TxtAdd2.Text = AgL.XNull(.Rows(0)("Add2"))
                TxtCity.AgSelectedValue = AgL.XNull(.Rows(0)("CityCode"))
                TxtPinNo.Text = AgL.XNull(.Rows(0)("PIN"))
                TxtMobile.Text = AgL.XNull(.Rows(0)("Mobile"))
                TxtPhone.Text = AgL.XNull(.Rows(0)("Phone"))
                TxtCreditDays.Text = AgL.XNull(.Rows(0)("CreditDays"))
                TxtCreditLimit.Text = AgL.XNull(.Rows(0)("CreditLimit"))
                TxtEMail.Text = AgL.XNull(.Rows(0)("EMail"))
                TxtSalesTaxGroup.AgSelectedValue = AgL.XNull(.Rows(0)("SalesTaxPostingGroup"))
                mNature = AgL.XNull(.Rows(0)("Nature"))
                mGroupNature = AgL.XNull(.Rows(0)("GroupNature"))



                Dgl1.Item(Col1Value, rowPanNo).Value = AgL.XNull(.Rows(0)("PAN"))
                Dgl1.Item(Col1Value, rowSalesTaxNo).Value = AgL.XNull(.Rows(0)("STRegNo"))
                Dgl1.Item(Col1Value, rowContactPerson).Value = AgL.XNull(.Rows(0)("ContactPerson"))
                Dgl1.Item(Col1Value, rowParent).Tag = AgL.XNull(.Rows(0)("Parent"))
                Dgl1.Item(Col1Value, rowParent).Value = AgL.XNull(.Rows(0)("ParentName"))
            End If
        End With
        Topctrl1.tPrn = False
    End Sub

    Private Sub Control_Enter(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Select Case sender.name
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Sub ProcSave()
        Dim MastPos As Long
        Dim mTrans As Boolean = False
        Dim ChildDataPassed As Boolean = True
        Dim bName$ = "", mUpLineStr$ = ""
        Try
            If AgL.PubMoveRecApplicable Then MastPos = BMBMaster.Position

            'For Data Validation
            If AgCL.AgCheckMandatory(Me) = False Then Exit Sub
            If AgL.RequiredField(TxtDispName, LblName.Text) Then Exit Sub

            If Not ChildDataPassed Then
                Exit Sub
            End If

            If Topctrl1.Mode = "Add" Then
                mSearchCode = AgL.GetMaxId("SubGroup", "SubCode", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 8, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)
                mInternalCode = mSearchCode
            End If

            If TxtAcGroup.Visible = False Then
                If mSubGroupNature = ESubgroupNature.Customer Then
                    TxtAcGroup.AgSelectedValue = SubGroupConst.GroupCode_Debtors
                    mGroupNature = SubGroupConst.GroupNature_Debtors
                    mNature = SubGroupConst.Nature_Debtors
                Else
                    TxtAcGroup.AgSelectedValue = SubGroupConst.GroupCode_Creditors
                    mGroupNature = SubGroupConst.GroupNature_Creditors
                    mNature = SubGroupConst.Nature_Creditors
                End If
            End If

            If AgL.RequiredField(TxtManualCode, LblManualCode.Text) Then Exit Sub

            If Topctrl1.Mode = "Add" Then
                mQry = "Select count(*) From SubGroup Where ManualCode='" & TxtManualCode.Text & "' And IfNull(IsDeleted,0)=0 AND Site_Code =" & AgL.Chk_Text(AgL.PubSiteCode) & ""
                If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Code Already Exists")
            Else
                mQry = "Select count(*) From SubGroup Where ManualCode ='" & TxtManualCode.Text & "' And SubCode<>'" & mInternalCode & "'  And IfNull(IsDeleted,0)=0 AND Site_Code =" & AgL.Chk_Text(AgL.PubSiteCode) & ""
                If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Code Already Exists")
            End If

            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = True

            bName = TxtDispName.Text + " {" + TxtManualCode.Text + "}"


            If Topctrl1.Mode = "Add" Then
                mQry = "INSERT INTO SubGroup(SubCode, SiteList, Site_Code, Name, DispName, " &
                        " GroupCode, GroupNature, MasterType,	ManualCode,	Nature,	Add1,	Add2,	CityCode,  " &
                        " PIN, Phone,  PAN, STRegNo, ContactPerson, SubgroupType, " &
                        " Mobile, CreditDays, CreditLimit, EMail, Parent, SalesTaxPostingGroup, " &
                        " EntryBy, EntryDate,  EntryType, EntryStatus, Div_Code, Status, " &
                        " U_Name, U_EntDt, U_AE) " &
                        " VALUES(" & AgL.Chk_Text(mSearchCode) & ", " &
                        " '|" & AgL.PubSiteCode & "|','" & AgL.PubSiteCode & "', " & AgL.Chk_Text(bName) & ",	" &
                        " " & AgL.Chk_Text(TxtDispName.Text) & ", " & AgL.Chk_Text(TxtAcGroup.AgSelectedValue) & ", " &
                        " " & AgL.Chk_Text(mGroupNature) & ", " & AgL.Chk_Text(mMasterType) & ", " & AgL.Chk_Text(TxtManualCode.Text) & ", " &
                        " " & AgL.Chk_Text(mNature) & ", " & AgL.Chk_Text(TxtAdd1.Text) & ", " &
                        " " & AgL.Chk_Text(TxtAdd2.Text) & ", " &
                        " " & AgL.Chk_Text(TxtCity.AgSelectedValue) & ", " &
                        " " & AgL.Chk_Text(TxtPinNo.Text) & ", " & AgL.Chk_Text(TxtPhone.Text) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowPanNo).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowSalesTaxNo).Value) & ", " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowContactPerson).Value) & ", " &
                        " " & AgL.Chk_Text(TxtPartyType.AgSelectedValue) & ", " &
                        " " & AgL.Chk_Text(TxtMobile.Text) & ", " &
                        " " & Val(TxtCreditDays.Text) & ", " &
                        " " & Val(TxtCreditLimit.Text) & ", " &
                        " " & AgL.Chk_Text(TxtEMail.Text) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowParent).Tag) & ", " &
                        " " & AgL.Chk_Text(TxtSalesTaxGroup.AgSelectedValue) & ", " &
                        " " & AgL.Chk_Text(AgL.PubUserName) & ", " & AgL.Chk_Text(AgL.GetDateTime(AgL.GcnRead)) & ", " &
                        " " & AgL.Chk_Text(Topctrl1.Mode) & ", " & AgL.Chk_Text(LogStatus.LogOpen) & ", " &
                        " " & AgL.Chk_Text(TxtDivision.AgSelectedValue) & ", " & AgL.Chk_Text(TxtStatus.Text) & ", " &
                        " '" & AgL.PubUserName & "'," & AgL.Chk_Date(CDate(AgL.PubLoginDate).ToString("u")) & ", 'A') "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            Else
                mQry = "UPDATE SubGroup " &
                        " SET " &
                        " Name = " & AgL.Chk_Text(bName) & ", " &
                        " DispName = " & AgL.Chk_Text(TxtDispName.Text) & ", " &
                        " GroupCode = " & AgL.Chk_Text(TxtAcGroup.AgSelectedValue) & ", " &
                        " GroupNature = " & AgL.Chk_Text(mGroupNature) & ", " &
                        " MasterType = " & AgL.Chk_Text(mMasterType) & ", " &
                        " ManualCode = " & AgL.Chk_Text(TxtManualCode.Text) & ", " &
                        " Nature = " & AgL.Chk_Text(mNature) & ", " &
                        " Add1 = " & AgL.Chk_Text(TxtAdd1.Text) & ", " &
                        " Add2 = " & AgL.Chk_Text(TxtAdd2.Text) & ", " &
                        " CityCode = " & AgL.Chk_Text(TxtCity.AgSelectedValue) & ", " &
                        " Mobile = " & AgL.Chk_Text(TxtMobile.Text) & ", " &
                        " CreditDays = " & Val(TxtCreditDays.Text) & ", " &
                        " CreditLimit = " & Val(TxtCreditLimit.Text) & ", " &
                        " EMail = " & AgL.Chk_Text(TxtEMail.Text) & ", " &
                        " PIN = " & AgL.Chk_Text(TxtPinNo.Text) & ", " &
                        " Phone = " & AgL.Chk_Text(TxtPhone.Text) & ", " &
                        " PAN = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowPanNo).Value) & ", " &
                        " STRegNo = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowSalesTaxNo).Value) & ", " &
                        " ContactPerson = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowContactPerson).Value) & ", " &
                        " Party_Type = " & AgL.Chk_Text(TxtPartyType.AgSelectedValue) & ", " &
                        " Parent = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowParent).Tag) & ", " &
                        " SalesTaxPostingGroup = " & AgL.Chk_Text(TxtSalesTaxGroup.AgSelectedValue) & ", " &
                        " EntryType = " & AgL.Chk_Text(Topctrl1.Mode) & ", " &
                        " EntryStatus = " & AgL.Chk_Text(LogStatus.LogOpen) & ", " &
                        " Div_Code = " & AgL.Chk_Text(TxtDivision.AgSelectedValue) & ", " &
                        " U_AE = 'E', " &
                        " Edit_Date = " & AgL.Chk_Date(CDate(AgL.PubLoginDate).ToString("u")) & ", " &
                        " ModifiedBy = '" & AgL.PubUserName & "' " &
                        " Where Subcode = " & AgL.Chk_Text(mSearchCode) & "  "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If



            Call AgL.LogTableEntry(mSearchCode, Me.Text, AgL.MidStr(Topctrl1.Mode, 0, 1), AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd)

            AgL.ETrans.Commit()
            mTrans = False


            If AgL.PubMoveRecApplicable Then
                FIniMaster(0, 1)
                Topctrl1_tbRef()
            End If

            If Topctrl1.Mode = "Add" Then
                If mIsReturnValue = True Then Me.Close() : Exit Sub
                Topctrl1.LblDocId.Text = mSearchCode
                Topctrl1.FButtonClick(0)
                Exit Sub
            Else
                Topctrl1.SetDisp(True)
                If AgL.PubMoveRecApplicable Then MoveRec()
            End If

        Catch ex As Exception
            If mTrans = True Then AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TxtCity_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtCity.Enter
        Select Case sender.name
            Case TxtCity.Name

        End Select
    End Sub

    Private Sub Control_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtCity.Validating, TxtAcGroup.Validating, TxtCity.Validating, TxtPartyType.Validating
        Dim DtTemp As DataTable = Nothing
        Dim DrTemp As DataRow() = Nothing
        Try
            Select Case sender.NAME
                Case TxtAcGroup.Name
                    If sender.text.ToString.Trim = "" Or sender.AgSelectedValue.Trim = "" Then
                        mGroupNature = ""
                        mNature = ""
                    Else
                        If sender.AgHelpDataSet IsNot Nothing Then
                            DrTemp = TxtAcGroup.AgHelpDataSet.Tables(0).Select("Code = " & AgL.Chk_Text(TxtAcGroup.AgSelectedValue) & "")
                            mGroupNature = AgL.XNull(DrTemp(0)("GroupNature"))
                            mNature = AgL.XNull(DrTemp(0)("Nature"))
                        End If
                    End If
                Case TxtPartyType.Name
                    ApplySubgroupTypeSetting(TxtPartyType.Text)
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmSteward_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsReturnValue = False Then
            AgL.WinSetting(Me, 418, 913, 0, 0)
        Else
            Topctrl1.FButtonClick(0)
        End If
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub TxtDescription_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtCreditLimit.KeyDown
        If e.KeyCode = Keys.Enter Then
            If MsgBox("Do you want to save?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Save") = MsgBoxResult.Yes Then
                Topctrl1.FButtonClick(13)
            End If
        End If
    End Sub

    Private Sub FrmParty_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        TxtSalesTaxGroup.Tag = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultSalesTaxGroupParty"))
        TxtSalesTaxGroup.Text = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultSalesTaxGroupParty"))

        'If mSubGroupNature = ESubgroupNature.Customer Then
        '    TxtAcGroup.AgSelectedValue = SubGroupConst.GroupCode_Debtors
        '    mNature = SubGroupConst.Nature_Debtors
        '    mGroupNature = SubGroupConst.GroupNature_Debtors
        'Else
        '    TxtAcGroup.AgSelectedValue = SubGroupConst.GroupCode_Creditors
        '    mNature = SubGroupConst.Nature_Creditors
        '    mGroupNature = SubGroupConst.GroupNature_Creditors
        'End If

        TxtPartyType.Focus()
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub FrmParty_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
        TxtManualCode.Focus()
    End Sub

    Private Function FRetUpline(ByVal SubCode As String, ByRef mUpLineStr As String, Optional ByVal Parent As String = "") As String
        Dim mParent As String = ""
        If Parent = "" Then
            mQry = " SELECT Sg.Parent FROM SubGroup Sg  WHERE Sg.SubCode = '" & SubCode & "'"
            mParent = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar)
        Else
            mParent = Parent
        End If

        If InStr(mUpLineStr, mSearchCode) > 0 Then
            Err.Raise(1, , "Parent Name Is Invalid.It is creating a cycle.")
        End If

        If mParent <> SubCode And mParent <> "" Then
            mUpLineStr += IIf(mUpLineStr = "", "|" + mParent + "|", "," + "|" + mParent + "|")
            FRetUpline(mParent, mUpLineStr)
        End If
        FRetUpline = mUpLineStr
    End Function

    Private Sub BtnImprtFromExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnImprtFromExcel.Click
        Dim DtTemp As DataTable
        Dim DtMain As DataTable = Nothing
        Dim I As Integer
        Dim CityCode$ = "", ErrorLog$ = ""
        Dim StrErrLog As String = ""
        mQry = "Select '' as Srl, 'Code' as [Field Name], 'Text' as [Data Type], 20 as [Length], 'Yes' As [Mandatory] "
        mQry = mQry + "Union All Select  '' as Srl,'Name' as [Field Name], 'Text' as [Data Type], 50 as [Length] , 'Yes' As [Mandatory] "
        mQry = mQry + "Union All Select  '' as Srl,'Add1' as [Field Name], 'Text' as [Data Type], 50 as [Length] , 'No' As [Mandatory] "
        mQry = mQry + "Union All Select  '' as Srl,'Add2' as [Field Name], 'Text' as [Data Type], 50 as [Length] , 'No' As [Mandatory] "
        mQry = mQry + "Union All Select  '' as Srl,'City' as [Field Name], 'Text' as [Data Type], 50 as [Length] , 'Yes' As [Mandatory] "
        mQry = mQry + "Union All Select  '' as Srl,'Pin' as [Field Name], 'Text' as [Data Type], 6 as [Length] , 'No' As [Mandatory] "
        mQry = mQry + "Union All Select  '' as Srl,'Contact No' as [Field Name], 'Text' as [Data Type], 35 as [Length] , 'No' As [Mandatory] "
        mQry = mQry + "Union All Select  '' as Srl,'Mobile' as [Field Name], 'Text' as [Data Type], 35 as [Length] , 'No' As [Mandatory] "
        mQry = mQry + "Union All Select  '' as Srl,'EMail' as [Field Name], 'Text' as [Data Type], 100 as [Length] , 'No' As [Mandatory] "
        mQry = mQry + "Union All Select  '' as Srl,'Contact Person' as [Field Name], 'Text' as [Data Type], 100 as [Length] , 'No' As [Mandatory] "
        mQry = mQry + "Union All Select  '' as Srl,'Fax No' as [Field Name], 'Text' as [Data Type], 35 as [Length] , 'No' As [Mandatory] "
        mQry = mQry + "Union All Select  '' as Srl,'CST No' as [Field Name], 'Text' as [Data Type], 40 as [Length] , 'No' As [Mandatory] "
        mQry = mQry + "Union All Select  '' as Srl,'TIN No' as [Field Name], 'Text' as [Data Type], 20 as [Length] , 'No' As [Mandatory] "
        mQry = mQry + "Union All Select  '' as Srl,'Service Tax No' as [Field Name], 'Text' as [Data Type], 40 as [Length] , 'No' As [Mandatory] "
        mQry = mQry + "Union All Select  '' as Srl,'Pan No' as [Field Name], 'Text' as [Data Type], 20 as [Length] , 'No' As [Mandatory] "
        mQry = mQry + "Union All Select  '' as Srl,'A/c Group' as [Field Name], 'Text' as [Data Type], 50 as [Length] , 'Yes' As [Mandatory] "

        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Dim ObjFrmImport As New FrmImportFromExcel
        ObjFrmImport.LblTitle.Text = "Item Master Import"
        ObjFrmImport.Dgl1.DataSource = DtTemp
        ObjFrmImport.ShowDialog()

        If Not AgL.StrCmp(ObjFrmImport.UserAction, "OK") Then Exit Sub

        DtTemp = ObjFrmImport.P_DsExcelData.Tables(0)


        For I = 0 To DtTemp.Rows.Count - 1
            If AgL.XNull(DtTemp.Rows(I)("A/c Group")) <> "" Then
                mQry = " Select GroupCode From AcGroup Where GroupName = " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("A/c Group"))) & " "
                If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar = 0 Then
                    If ErrorLog = "" Then
                        ErrorLog = vbCrLf & "These A/c Groups Are Not Present In Master" & vbCrLf
                        ErrorLog += AgL.XNull(DtTemp.Rows(I)("A/c Group")) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtTemp.Rows(I)("A/c Group")) & ", "
                    End If
                End If
            End If
        Next

        If ErrorLog <> "" Then
            If File.Exists(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt") Then
                My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath + "\" + "ErrorLog.txt", ErrorLog, False)
            Else
                File.Create(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt")
                My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt", ErrorLog, False)
            End If
            System.Diagnostics.Process.Start("notepad.exe", My.Application.Info.DirectoryPath + "\" + "ErrorLog.txt")
            Exit Sub
        End If

        For I = 0 To DtTemp.Rows.Count - 1
            Topctrl1.FButtonClick(0)

            If DtTemp.Columns.Contains("Code") Then TxtManualCode.Text = AgL.XNull(DtTemp.Rows(I)("Code")) Else MsgBox("Code Is Mandatory") : Exit Sub
            If DtTemp.Columns.Contains("Name") Then TxtDispName.Text = AgL.XNull(DtTemp.Rows(I)("Name")) Else MsgBox("Name Is Mandatory") : Exit Sub
            If DtTemp.Columns.Contains("Add1") Then TxtAdd1.Text = AgL.XNull(DtTemp.Rows(I)("Add1"))
            If DtTemp.Columns.Contains("Add2") Then TxtAdd2.Text = AgL.XNull(DtTemp.Rows(I)("Add2"))

            If DtTemp.Columns.Contains("City") Then TxtCity.Text = AgL.XNull(DtTemp.Rows(I)("City")) Else MsgBox("City Is Mandatory") : Exit Sub

            If AgL.VNull(AgL.Dman_Execute("Select Count(*) From City Where CityName = '" & TxtCity.Text & "'", AgL.GcnRead).ExecuteScalar) = 0 Then
                CityCode = AgL.GetMaxId("City", "CityCode", AgL.GcnRead, AgL.PubDivCode, AgL.PubSiteCode)
                mQry = " INSERT INTO City(CityCode,CityName) VALUES(" & AgL.Chk_Text(CityCode) & ", " & AgL.Chk_Text(TxtCity.Text) & ") "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If


            If DtTemp.Columns.Contains("Pin") Then TxtPinNo.Text = AgL.XNull(DtTemp.Rows(I)("Pin"))
            If DtTemp.Columns.Contains("Contact No") Then TxtPhone.Text = AgL.XNull(DtTemp.Rows(I)("Contact No"))
            If DtTemp.Columns.Contains("Mobile") Then TxtMobile.Text = AgL.XNull(DtTemp.Rows(I)("Mobile"))
            If DtTemp.Columns.Contains("EMail") Then TxtEMail.Text = AgL.XNull(DtTemp.Rows(I)("EMail"))
            If DtTemp.Columns.Contains("Contact Person") Then Dgl1.Item(Col1Value, rowContactPerson).Value = AgL.XNull(DtTemp.Rows(I)("Contact Person"))

            If DtTemp.Columns.Contains("STRegNo") Then Dgl1.Item(Col1Value, rowContactPerson) = AgL.XNull(DtTemp.Rows(I)("GST NO"))
            If DtTemp.Columns.Contains("Pan No") Then Dgl1.Item(Col1Value, rowPanNo) = AgL.XNull(DtTemp.Rows(I)("Pan No"))
            If DtTemp.Columns.Contains("A/c Group") Then TxtAcGroup.Text = AgL.XNull(DtTemp.Rows(I)("A/c Group")) Else MsgBox("A/c Group Is Mandatory") : Exit Sub
            If TxtAcGroup.Text <> "" Then TxtAcGroup.Tag = AgL.XNull(AgL.Dman_Execute("Select GroupCode From AcGroup Where GroupName = '" & TxtAcGroup.Text & "' ", AgL.GcnRead).ExecuteScalar)

            Topctrl1.FButtonClick(13)
        Next
    End Sub

    Private Sub FrmParty_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        GBoxImportFromExcel.Enabled = True
    End Sub

    Private Sub FrmPerson_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgTextColumn(Dgl1, Col1Head, 150, 255, Col1Head, True, True)
            .AddAgTextColumn(Dgl1, Col1Mandatory, 10, 20, Col1Mandatory, True, True)
            .AddAgTextColumn(Dgl1, Col1Value, 220, 255, Col1Value, True, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToAddRows = False
        Dgl1.RowHeadersVisible = False
        Dgl1.ColumnHeadersVisible = False


        Dgl1.Rows.Add(5)
        Dgl1.Item(Col1Head, rowContactPerson).Value = "Contact Person"
        Dgl1.Item(Col1Head, rowPanNo).Value = "PAN No."
        Dgl1.Item(Col1Head, rowSalesTaxNo).Value = "GST No."
        Dgl1.Item(Col1Head, rowAadharNo).Value = "Aadhar No."
        Dgl1.Item(Col1Head, rowParent).Value = "Master Party"
    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Try

            If Dgl1.CurrentCell Is Nothing Then Exit Sub
            If Topctrl1.Mode = "BROWSE" Then
                Dgl1.CurrentCell.ReadOnly = True
            End If

            If Dgl1.CurrentCell.ColumnIndex <> Dgl1.Columns(Col1Value).Index Then Exit Sub


            Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) = Nothing
            CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Text_Value
            CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 0

            Select Case Dgl1.CurrentCell.RowIndex
                Case rowContactPerson
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 100
                Case rowPanNo
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 10
                Case rowAadharNo
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 12
                Case rowSalesTaxNo
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 15
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Try
            bRowIndex = Dgl1.CurrentCell.RowIndex
            bColumnIndex = Dgl1.CurrentCell.ColumnIndex

            If e.KeyCode = Keys.Enter Then Exit Sub
            If bColumnIndex <> Dgl1.Columns(Col1Value).Index Then Exit Sub

            Select Case Dgl1.CurrentCell.RowIndex
                Case rowParent
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "select Code, Name From viewHelpSubgroup Where Code<>'" & mSearchCode & "' Order By Name"
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmPerson_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dim I As Integer
        For I = 0 To Dgl1.Rows.Count - 1
            Dgl1.Item(Col1Value, I).Value = ""
            Dgl1.Item(Col1Value, I).Tag = ""
        Next
    End Sub
End Class

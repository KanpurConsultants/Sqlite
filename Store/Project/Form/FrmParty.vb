Public Class FrmParty
    Inherits AgTemplate.TempMaster
    Dim mQry$ = ""
    Protected Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Protected Const Col1Process As String = "Process"

    Protected mGroupNature As String = "", mNature As String = ""
    Dim mMasterType$ = ""
    Protected WithEvents TxtCurrency As AgControls.AgTextBox
    Protected WithEvents Label1 As System.Windows.Forms.Label

    Dim mSubGroupNature As ESubgroupNature
    Protected WithEvents Label2 As System.Windows.Forms.Label
    Protected WithEvents Label3 As System.Windows.Forms.Label
    Protected WithEvents Pnl1 As System.Windows.Forms.Panel
    Protected WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Protected WithEvents TxtWorkinBranch As AgControls.AgTextBox
    Protected WithEvents Label9 As System.Windows.Forms.Label
    Protected WithEvents TxtWorkDivision As AgControls.AgTextBox
    Protected WithEvents Label8 As System.Windows.Forms.Label
    Dim mIsReturnValue As Boolean = False

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
        Me.TxtEMail = New AgControls.AgTextBox
        Me.LblEMail = New System.Windows.Forms.Label
        Me.TxtMobile = New AgControls.AgTextBox
        Me.LblMobile = New System.Windows.Forms.Label
        Me.LblCityReq = New System.Windows.Forms.Label
        Me.TxtCity = New AgControls.AgTextBox
        Me.LblCity = New System.Windows.Forms.Label
        Me.LblAddressReq = New System.Windows.Forms.Label
        Me.TxtAdd2 = New AgControls.AgTextBox
        Me.TxtAdd1 = New AgControls.AgTextBox
        Me.LblAddress = New System.Windows.Forms.Label
        Me.LblNameReq = New System.Windows.Forms.Label
        Me.LblManualCodeReq = New System.Windows.Forms.Label
        Me.TxtManualCode = New AgControls.AgTextBox
        Me.LblManualCode = New System.Windows.Forms.Label
        Me.TxtDispName = New AgControls.AgTextBox
        Me.LblName = New System.Windows.Forms.Label
        Me.TxtAcGroup = New AgControls.AgTextBox
        Me.LblAcGroup = New System.Windows.Forms.Label
        Me.LblAcGroupReq = New System.Windows.Forms.Label
        Me.TxtCreditDays = New AgControls.AgTextBox
        Me.LblCreditDays = New System.Windows.Forms.Label
        Me.TxtCreditLimit = New AgControls.AgTextBox
        Me.LblCreditLimit = New System.Windows.Forms.Label
        Me.GrpCreditDetail = New System.Windows.Forms.GroupBox
        Me.TxtFax = New AgControls.AgTextBox
        Me.LblBuyerFax = New System.Windows.Forms.Label
        Me.TxtPhone = New AgControls.AgTextBox
        Me.LblPhone = New System.Windows.Forms.Label
        Me.TxtSalesTaxGroup = New AgControls.AgTextBox
        Me.LblSalesTaxGroup = New System.Windows.Forms.Label
        Me.TxtCSTNo = New AgControls.AgTextBox
        Me.LblCSTNo = New System.Windows.Forms.Label
        Me.TxtTinNo = New AgControls.AgTextBox
        Me.LblTinNo = New System.Windows.Forms.Label
        Me.TxtPanNo = New AgControls.AgTextBox
        Me.LblPanNo = New System.Windows.Forms.Label
        Me.TxtStRegNo = New AgControls.AgTextBox
        Me.LblStRegNo = New System.Windows.Forms.Label
        Me.TxtContactPerson = New AgControls.AgTextBox
        Me.LblContactPerson = New System.Windows.Forms.Label
        Me.TxtCostCenter = New AgControls.AgTextBox
        Me.LblCostCenter = New System.Windows.Forms.Label
        Me.TxtUnderSubCode = New AgControls.AgTextBox
        Me.LblUnderSubCode = New System.Windows.Forms.Label
        Me.TxtPinNo = New AgControls.AgTextBox
        Me.LblPinNo = New System.Windows.Forms.Label
        Me.TxtPartyType = New AgControls.AgTextBox
        Me.LblPartyType = New System.Windows.Forms.Label
        Me.TxtCurrency = New AgControls.AgTextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Pnl1 = New System.Windows.Forms.Panel
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
        Me.TxtWorkinBranch = New AgControls.AgTextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.TxtWorkDivision = New AgControls.AgTextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.GrpUP.SuspendLayout()
        Me.GBoxEntryType.SuspendLayout()
        Me.GBoxMoveToLog.SuspendLayout()
        Me.GBoxApprove.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GBoxDivision.SuspendLayout()
        CType(Me.DTMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpCreditDetail.SuspendLayout()
        Me.SuspendLayout()
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(907, 41)
        Me.Topctrl1.TabIndex = 22
        Me.Topctrl1.tAdd = False
        Me.Topctrl1.tDel = False
        Me.Topctrl1.tEdit = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(0, 374)
        Me.GroupBox1.Size = New System.Drawing.Size(949, 4)
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(6, 378)
        '
        'TxtEntryBy
        '
        Me.TxtEntryBy.Tag = ""
        Me.TxtEntryBy.Text = ""
        '
        'GBoxEntryType
        '
        Me.GBoxEntryType.Location = New System.Drawing.Point(142, 378)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Tag = ""
        '
        'GBoxMoveToLog
        '
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(556, 378)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Tag = ""
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(400, 378)
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
        Me.GroupBox2.Location = New System.Drawing.Point(702, 378)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(271, 378)
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
        Me.TxtEMail.Location = New System.Drawing.Point(142, 172)
        Me.TxtEMail.MaxLength = 100
        Me.TxtEMail.Name = "TxtEMail"
        Me.TxtEMail.Size = New System.Drawing.Size(292, 18)
        Me.TxtEMail.TabIndex = 8
        '
        'LblEMail
        '
        Me.LblEMail.AutoSize = True
        Me.LblEMail.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblEMail.Location = New System.Drawing.Point(20, 173)
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
        Me.TxtMobile.Location = New System.Drawing.Point(321, 152)
        Me.TxtMobile.MaxLength = 35
        Me.TxtMobile.Name = "TxtMobile"
        Me.TxtMobile.Size = New System.Drawing.Size(113, 18)
        Me.TxtMobile.TabIndex = 7
        '
        'LblMobile
        '
        Me.LblMobile.AutoSize = True
        Me.LblMobile.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMobile.Location = New System.Drawing.Point(262, 153)
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
        Me.LblCityReq.Location = New System.Drawing.Point(125, 139)
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
        Me.TxtCity.Location = New System.Drawing.Point(142, 132)
        Me.TxtCity.MaxLength = 0
        Me.TxtCity.Name = "TxtCity"
        Me.TxtCity.Size = New System.Drawing.Size(115, 18)
        Me.TxtCity.TabIndex = 4
        '
        'LblCity
        '
        Me.LblCity.AutoSize = True
        Me.LblCity.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCity.Location = New System.Drawing.Point(20, 132)
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
        Me.LblAddressReq.Location = New System.Drawing.Point(125, 100)
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
        Me.TxtAdd2.Location = New System.Drawing.Point(142, 112)
        Me.TxtAdd2.MaxLength = 50
        Me.TxtAdd2.Name = "TxtAdd2"
        Me.TxtAdd2.Size = New System.Drawing.Size(292, 18)
        Me.TxtAdd2.TabIndex = 3
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
        Me.TxtAdd1.Location = New System.Drawing.Point(142, 92)
        Me.TxtAdd1.MaxLength = 50
        Me.TxtAdd1.Name = "TxtAdd1"
        Me.TxtAdd1.Size = New System.Drawing.Size(292, 18)
        Me.TxtAdd1.TabIndex = 2
        '
        'LblAddress
        '
        Me.LblAddress.AutoSize = True
        Me.LblAddress.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblAddress.Location = New System.Drawing.Point(20, 92)
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
        Me.LblNameReq.Location = New System.Drawing.Point(125, 79)
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
        Me.LblManualCodeReq.Location = New System.Drawing.Point(125, 59)
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
        Me.TxtManualCode.Location = New System.Drawing.Point(142, 52)
        Me.TxtManualCode.MaxLength = 20
        Me.TxtManualCode.Name = "TxtManualCode"
        Me.TxtManualCode.Size = New System.Drawing.Size(171, 18)
        Me.TxtManualCode.TabIndex = 0
        '
        'LblManualCode
        '
        Me.LblManualCode.AutoSize = True
        Me.LblManualCode.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblManualCode.Location = New System.Drawing.Point(20, 52)
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
        Me.TxtDispName.Location = New System.Drawing.Point(142, 72)
        Me.TxtDispName.MaxLength = 100
        Me.TxtDispName.Name = "TxtDispName"
        Me.TxtDispName.Size = New System.Drawing.Size(292, 18)
        Me.TxtDispName.TabIndex = 1
        '
        'LblName
        '
        Me.LblName.AutoSize = True
        Me.LblName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblName.Location = New System.Drawing.Point(20, 72)
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
        Me.TxtAcGroup.Location = New System.Drawing.Point(142, 291)
        Me.TxtAcGroup.MaxLength = 100
        Me.TxtAcGroup.Name = "TxtAcGroup"
        Me.TxtAcGroup.Size = New System.Drawing.Size(292, 18)
        Me.TxtAcGroup.TabIndex = 17
        '
        'LblAcGroup
        '
        Me.LblAcGroup.AutoSize = True
        Me.LblAcGroup.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblAcGroup.Location = New System.Drawing.Point(20, 291)
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
        Me.LblAcGroupReq.Location = New System.Drawing.Point(125, 297)
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
        Me.TxtCreditDays.Location = New System.Drawing.Point(117, 19)
        Me.TxtCreditDays.MaxLength = 5
        Me.TxtCreditDays.Name = "TxtCreditDays"
        Me.TxtCreditDays.Size = New System.Drawing.Size(77, 18)
        Me.TxtCreditDays.TabIndex = 0
        '
        'LblCreditDays
        '
        Me.LblCreditDays.AutoSize = True
        Me.LblCreditDays.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCreditDays.Location = New System.Drawing.Point(22, 20)
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
        Me.TxtCreditLimit.Location = New System.Drawing.Point(303, 18)
        Me.TxtCreditLimit.MaxLength = 10
        Me.TxtCreditLimit.Name = "TxtCreditLimit"
        Me.TxtCreditLimit.Size = New System.Drawing.Size(77, 18)
        Me.TxtCreditLimit.TabIndex = 1
        '
        'LblCreditLimit
        '
        Me.LblCreditLimit.AutoSize = True
        Me.LblCreditLimit.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCreditLimit.Location = New System.Drawing.Point(220, 19)
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
        Me.GrpCreditDetail.Location = New System.Drawing.Point(467, 135)
        Me.GrpCreditDetail.Name = "GrpCreditDetail"
        Me.GrpCreditDetail.Size = New System.Drawing.Size(407, 56)
        Me.GrpCreditDetail.TabIndex = 21
        Me.GrpCreditDetail.TabStop = False
        Me.GrpCreditDetail.Text = "Credit Detail"
        '
        'TxtFax
        '
        Me.TxtFax.AgAllowUserToEnableMasterHelp = False
        Me.TxtFax.AgLastValueTag = Nothing
        Me.TxtFax.AgLastValueText = Nothing
        Me.TxtFax.AgMandatory = False
        Me.TxtFax.AgMasterHelp = False
        Me.TxtFax.AgNumberLeftPlaces = 0
        Me.TxtFax.AgNumberNegetiveAllow = False
        Me.TxtFax.AgNumberRightPlaces = 0
        Me.TxtFax.AgPickFromLastValue = False
        Me.TxtFax.AgRowFilter = ""
        Me.TxtFax.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtFax.AgSelectedValue = Nothing
        Me.TxtFax.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtFax.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtFax.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtFax.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFax.Location = New System.Drawing.Point(142, 232)
        Me.TxtFax.MaxLength = 35
        Me.TxtFax.Name = "TxtFax"
        Me.TxtFax.Size = New System.Drawing.Size(292, 18)
        Me.TxtFax.TabIndex = 12
        '
        'LblBuyerFax
        '
        Me.LblBuyerFax.AutoSize = True
        Me.LblBuyerFax.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBuyerFax.Location = New System.Drawing.Point(20, 232)
        Me.LblBuyerFax.Name = "LblBuyerFax"
        Me.LblBuyerFax.Size = New System.Drawing.Size(54, 16)
        Me.LblBuyerFax.TabIndex = 865
        Me.LblBuyerFax.Text = "Fax No,"
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
        Me.TxtPhone.Location = New System.Drawing.Point(142, 152)
        Me.TxtPhone.MaxLength = 35
        Me.TxtPhone.Name = "TxtPhone"
        Me.TxtPhone.Size = New System.Drawing.Size(115, 18)
        Me.TxtPhone.TabIndex = 6
        '
        'LblPhone
        '
        Me.LblPhone.AutoSize = True
        Me.LblPhone.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPhone.Location = New System.Drawing.Point(20, 153)
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
        Me.TxtSalesTaxGroup.Location = New System.Drawing.Point(142, 192)
        Me.TxtSalesTaxGroup.MaxLength = 20
        Me.TxtSalesTaxGroup.Name = "TxtSalesTaxGroup"
        Me.TxtSalesTaxGroup.Size = New System.Drawing.Size(115, 18)
        Me.TxtSalesTaxGroup.TabIndex = 9
        '
        'LblSalesTaxGroup
        '
        Me.LblSalesTaxGroup.AutoSize = True
        Me.LblSalesTaxGroup.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblSalesTaxGroup.Location = New System.Drawing.Point(20, 193)
        Me.LblSalesTaxGroup.Name = "LblSalesTaxGroup"
        Me.LblSalesTaxGroup.Size = New System.Drawing.Size(104, 16)
        Me.LblSalesTaxGroup.TabIndex = 867
        Me.LblSalesTaxGroup.Text = "Sales Tax Group"
        '
        'TxtCSTNo
        '
        Me.TxtCSTNo.AgAllowUserToEnableMasterHelp = False
        Me.TxtCSTNo.AgLastValueTag = Nothing
        Me.TxtCSTNo.AgLastValueText = Nothing
        Me.TxtCSTNo.AgMandatory = False
        Me.TxtCSTNo.AgMasterHelp = False
        Me.TxtCSTNo.AgNumberLeftPlaces = 0
        Me.TxtCSTNo.AgNumberNegetiveAllow = False
        Me.TxtCSTNo.AgNumberRightPlaces = 0
        Me.TxtCSTNo.AgPickFromLastValue = False
        Me.TxtCSTNo.AgRowFilter = ""
        Me.TxtCSTNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCSTNo.AgSelectedValue = Nothing
        Me.TxtCSTNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCSTNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtCSTNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtCSTNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCSTNo.Location = New System.Drawing.Point(142, 252)
        Me.TxtCSTNo.MaxLength = 35
        Me.TxtCSTNo.Name = "TxtCSTNo"
        Me.TxtCSTNo.Size = New System.Drawing.Size(110, 18)
        Me.TxtCSTNo.TabIndex = 13
        '
        'LblCSTNo
        '
        Me.LblCSTNo.AutoSize = True
        Me.LblCSTNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCSTNo.Location = New System.Drawing.Point(20, 253)
        Me.LblCSTNo.Name = "LblCSTNo"
        Me.LblCSTNo.Size = New System.Drawing.Size(53, 16)
        Me.LblCSTNo.TabIndex = 869
        Me.LblCSTNo.Text = "CST No"
        '
        'TxtTinNo
        '
        Me.TxtTinNo.AgAllowUserToEnableMasterHelp = False
        Me.TxtTinNo.AgLastValueTag = Nothing
        Me.TxtTinNo.AgLastValueText = Nothing
        Me.TxtTinNo.AgMandatory = False
        Me.TxtTinNo.AgMasterHelp = False
        Me.TxtTinNo.AgNumberLeftPlaces = 0
        Me.TxtTinNo.AgNumberNegetiveAllow = False
        Me.TxtTinNo.AgNumberRightPlaces = 0
        Me.TxtTinNo.AgPickFromLastValue = False
        Me.TxtTinNo.AgRowFilter = ""
        Me.TxtTinNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtTinNo.AgSelectedValue = Nothing
        Me.TxtTinNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtTinNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtTinNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtTinNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTinNo.Location = New System.Drawing.Point(318, 252)
        Me.TxtTinNo.MaxLength = 20
        Me.TxtTinNo.Name = "TxtTinNo"
        Me.TxtTinNo.Size = New System.Drawing.Size(116, 18)
        Me.TxtTinNo.TabIndex = 14
        '
        'LblTinNo
        '
        Me.LblTinNo.AutoSize = True
        Me.LblTinNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTinNo.Location = New System.Drawing.Point(258, 252)
        Me.LblTinNo.Name = "LblTinNo"
        Me.LblTinNo.Size = New System.Drawing.Size(47, 16)
        Me.LblTinNo.TabIndex = 871
        Me.LblTinNo.Text = "TIN No"
        '
        'TxtPanNo
        '
        Me.TxtPanNo.AgAllowUserToEnableMasterHelp = False
        Me.TxtPanNo.AgLastValueTag = Nothing
        Me.TxtPanNo.AgLastValueText = Nothing
        Me.TxtPanNo.AgMandatory = False
        Me.TxtPanNo.AgMasterHelp = False
        Me.TxtPanNo.AgNumberLeftPlaces = 0
        Me.TxtPanNo.AgNumberNegetiveAllow = False
        Me.TxtPanNo.AgNumberRightPlaces = 0
        Me.TxtPanNo.AgPickFromLastValue = False
        Me.TxtPanNo.AgRowFilter = ""
        Me.TxtPanNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtPanNo.AgSelectedValue = Nothing
        Me.TxtPanNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPanNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtPanNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPanNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPanNo.Location = New System.Drawing.Point(318, 271)
        Me.TxtPanNo.MaxLength = 20
        Me.TxtPanNo.Name = "TxtPanNo"
        Me.TxtPanNo.Size = New System.Drawing.Size(116, 18)
        Me.TxtPanNo.TabIndex = 16
        '
        'LblPanNo
        '
        Me.LblPanNo.AutoSize = True
        Me.LblPanNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPanNo.Location = New System.Drawing.Point(260, 273)
        Me.LblPanNo.Name = "LblPanNo"
        Me.LblPanNo.Size = New System.Drawing.Size(51, 16)
        Me.LblPanNo.TabIndex = 873
        Me.LblPanNo.Text = "Pan No"
        '
        'TxtStRegNo
        '
        Me.TxtStRegNo.AgAllowUserToEnableMasterHelp = False
        Me.TxtStRegNo.AgLastValueTag = Nothing
        Me.TxtStRegNo.AgLastValueText = Nothing
        Me.TxtStRegNo.AgMandatory = False
        Me.TxtStRegNo.AgMasterHelp = False
        Me.TxtStRegNo.AgNumberLeftPlaces = 0
        Me.TxtStRegNo.AgNumberNegetiveAllow = False
        Me.TxtStRegNo.AgNumberRightPlaces = 0
        Me.TxtStRegNo.AgPickFromLastValue = False
        Me.TxtStRegNo.AgRowFilter = ""
        Me.TxtStRegNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtStRegNo.AgSelectedValue = Nothing
        Me.TxtStRegNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtStRegNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtStRegNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtStRegNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtStRegNo.Location = New System.Drawing.Point(142, 272)
        Me.TxtStRegNo.MaxLength = 25
        Me.TxtStRegNo.Name = "TxtStRegNo"
        Me.TxtStRegNo.Size = New System.Drawing.Size(110, 18)
        Me.TxtStRegNo.TabIndex = 15
        '
        'LblStRegNo
        '
        Me.LblStRegNo.AutoSize = True
        Me.LblStRegNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblStRegNo.Location = New System.Drawing.Point(20, 275)
        Me.LblStRegNo.Name = "LblStRegNo"
        Me.LblStRegNo.Size = New System.Drawing.Size(94, 16)
        Me.LblStRegNo.TabIndex = 875
        Me.LblStRegNo.Text = "Service Tax No"
        '
        'TxtContactPerson
        '
        Me.TxtContactPerson.AgAllowUserToEnableMasterHelp = False
        Me.TxtContactPerson.AgLastValueTag = Nothing
        Me.TxtContactPerson.AgLastValueText = Nothing
        Me.TxtContactPerson.AgMandatory = False
        Me.TxtContactPerson.AgMasterHelp = False
        Me.TxtContactPerson.AgNumberLeftPlaces = 0
        Me.TxtContactPerson.AgNumberNegetiveAllow = False
        Me.TxtContactPerson.AgNumberRightPlaces = 0
        Me.TxtContactPerson.AgPickFromLastValue = False
        Me.TxtContactPerson.AgRowFilter = ""
        Me.TxtContactPerson.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtContactPerson.AgSelectedValue = Nothing
        Me.TxtContactPerson.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtContactPerson.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtContactPerson.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtContactPerson.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtContactPerson.Location = New System.Drawing.Point(142, 212)
        Me.TxtContactPerson.MaxLength = 50
        Me.TxtContactPerson.Name = "TxtContactPerson"
        Me.TxtContactPerson.Size = New System.Drawing.Size(292, 18)
        Me.TxtContactPerson.TabIndex = 11
        '
        'LblContactPerson
        '
        Me.LblContactPerson.AutoSize = True
        Me.LblContactPerson.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblContactPerson.Location = New System.Drawing.Point(20, 213)
        Me.LblContactPerson.Name = "LblContactPerson"
        Me.LblContactPerson.Size = New System.Drawing.Size(98, 16)
        Me.LblContactPerson.TabIndex = 877
        Me.LblContactPerson.Text = "Contact Person"
        '
        'TxtCostCenter
        '
        Me.TxtCostCenter.AgAllowUserToEnableMasterHelp = False
        Me.TxtCostCenter.AgLastValueTag = Nothing
        Me.TxtCostCenter.AgLastValueText = Nothing
        Me.TxtCostCenter.AgMandatory = False
        Me.TxtCostCenter.AgMasterHelp = False
        Me.TxtCostCenter.AgNumberLeftPlaces = 0
        Me.TxtCostCenter.AgNumberNegetiveAllow = False
        Me.TxtCostCenter.AgNumberRightPlaces = 0
        Me.TxtCostCenter.AgPickFromLastValue = False
        Me.TxtCostCenter.AgRowFilter = ""
        Me.TxtCostCenter.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCostCenter.AgSelectedValue = Nothing
        Me.TxtCostCenter.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCostCenter.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtCostCenter.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtCostCenter.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCostCenter.Location = New System.Drawing.Point(339, 311)
        Me.TxtCostCenter.MaxLength = 50
        Me.TxtCostCenter.Name = "TxtCostCenter"
        Me.TxtCostCenter.Size = New System.Drawing.Size(95, 18)
        Me.TxtCostCenter.TabIndex = 19
        '
        'LblCostCenter
        '
        Me.LblCostCenter.AutoSize = True
        Me.LblCostCenter.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCostCenter.Location = New System.Drawing.Point(256, 312)
        Me.LblCostCenter.Name = "LblCostCenter"
        Me.LblCostCenter.Size = New System.Drawing.Size(77, 16)
        Me.LblCostCenter.TabIndex = 879
        Me.LblCostCenter.Text = "Cost Center"
        '
        'TxtUnderSubCode
        '
        Me.TxtUnderSubCode.AgAllowUserToEnableMasterHelp = False
        Me.TxtUnderSubCode.AgLastValueTag = Nothing
        Me.TxtUnderSubCode.AgLastValueText = Nothing
        Me.TxtUnderSubCode.AgMandatory = False
        Me.TxtUnderSubCode.AgMasterHelp = False
        Me.TxtUnderSubCode.AgNumberLeftPlaces = 0
        Me.TxtUnderSubCode.AgNumberNegetiveAllow = False
        Me.TxtUnderSubCode.AgNumberRightPlaces = 0
        Me.TxtUnderSubCode.AgPickFromLastValue = False
        Me.TxtUnderSubCode.AgRowFilter = ""
        Me.TxtUnderSubCode.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtUnderSubCode.AgSelectedValue = Nothing
        Me.TxtUnderSubCode.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtUnderSubCode.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtUnderSubCode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtUnderSubCode.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtUnderSubCode.Location = New System.Drawing.Point(142, 331)
        Me.TxtUnderSubCode.MaxLength = 20
        Me.TxtUnderSubCode.Name = "TxtUnderSubCode"
        Me.TxtUnderSubCode.Size = New System.Drawing.Size(292, 18)
        Me.TxtUnderSubCode.TabIndex = 20
        '
        'LblUnderSubCode
        '
        Me.LblUnderSubCode.AutoSize = True
        Me.LblUnderSubCode.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblUnderSubCode.Location = New System.Drawing.Point(20, 332)
        Me.LblUnderSubCode.Name = "LblUnderSubCode"
        Me.LblUnderSubCode.Size = New System.Drawing.Size(84, 16)
        Me.LblUnderSubCode.TabIndex = 883
        Me.LblUnderSubCode.Text = "Parent Name"
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
        Me.TxtPinNo.Location = New System.Drawing.Point(321, 132)
        Me.TxtPinNo.MaxLength = 6
        Me.TxtPinNo.Name = "TxtPinNo"
        Me.TxtPinNo.Size = New System.Drawing.Size(113, 18)
        Me.TxtPinNo.TabIndex = 5
        '
        'LblPinNo
        '
        Me.LblPinNo.AutoSize = True
        Me.LblPinNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPinNo.Location = New System.Drawing.Point(262, 133)
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
        Me.TxtPartyType.Location = New System.Drawing.Point(142, 311)
        Me.TxtPartyType.MaxLength = 50
        Me.TxtPartyType.Name = "TxtPartyType"
        Me.TxtPartyType.Size = New System.Drawing.Size(110, 18)
        Me.TxtPartyType.TabIndex = 18
        '
        'LblPartyType
        '
        Me.LblPartyType.AutoSize = True
        Me.LblPartyType.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPartyType.Location = New System.Drawing.Point(20, 312)
        Me.LblPartyType.Name = "LblPartyType"
        Me.LblPartyType.Size = New System.Drawing.Size(70, 16)
        Me.LblPartyType.TabIndex = 887
        Me.LblPartyType.Text = "Party Type"
        '
        'TxtCurrency
        '
        Me.TxtCurrency.AgAllowUserToEnableMasterHelp = False
        Me.TxtCurrency.AgLastValueTag = Nothing
        Me.TxtCurrency.AgLastValueText = Nothing
        Me.TxtCurrency.AgMandatory = False
        Me.TxtCurrency.AgMasterHelp = False
        Me.TxtCurrency.AgNumberLeftPlaces = 0
        Me.TxtCurrency.AgNumberNegetiveAllow = False
        Me.TxtCurrency.AgNumberRightPlaces = 0
        Me.TxtCurrency.AgPickFromLastValue = False
        Me.TxtCurrency.AgRowFilter = ""
        Me.TxtCurrency.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCurrency.AgSelectedValue = Nothing
        Me.TxtCurrency.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCurrency.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtCurrency.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtCurrency.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCurrency.Location = New System.Drawing.Point(341, 192)
        Me.TxtCurrency.MaxLength = 35
        Me.TxtCurrency.Name = "TxtCurrency"
        Me.TxtCurrency.Size = New System.Drawing.Size(93, 18)
        Me.TxtCurrency.TabIndex = 10
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(262, 193)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 16)
        Me.Label1.TabIndex = 889
        Me.Label1.Text = "Currency"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(125, 198)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(10, 7)
        Me.Label2.TabIndex = 890
        Me.Label2.Text = "Ä"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(322, 198)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(10, 7)
        Me.Label3.TabIndex = 891
        Me.Label3.Text = "Ä"
        '
        'Pnl1
        '
        Me.Pnl1.Location = New System.Drawing.Point(464, 218)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(414, 147)
        Me.Pnl1.TabIndex = 894
        Me.Pnl1.Visible = False
        '
        'LinkLabel1
        '
        Me.LinkLabel1.BackColor = System.Drawing.Color.SteelBlue
        Me.LinkLabel1.DisabledLinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel1.LinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Location = New System.Drawing.Point(464, 197)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(105, 20)
        Me.LinkLabel1.TabIndex = 895
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Process Detail"
        Me.LinkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.LinkLabel1.Visible = False
        '
        'TxtWorkinBranch
        '
        Me.TxtWorkinBranch.AgAllowUserToEnableMasterHelp = False
        Me.TxtWorkinBranch.AgLastValueTag = Nothing
        Me.TxtWorkinBranch.AgLastValueText = Nothing
        Me.TxtWorkinBranch.AgMandatory = False
        Me.TxtWorkinBranch.AgMasterHelp = False
        Me.TxtWorkinBranch.AgNumberLeftPlaces = 0
        Me.TxtWorkinBranch.AgNumberNegetiveAllow = False
        Me.TxtWorkinBranch.AgNumberRightPlaces = 0
        Me.TxtWorkinBranch.AgPickFromLastValue = False
        Me.TxtWorkinBranch.AgRowFilter = ""
        Me.TxtWorkinBranch.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtWorkinBranch.AgSelectedValue = Nothing
        Me.TxtWorkinBranch.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtWorkinBranch.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtWorkinBranch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtWorkinBranch.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtWorkinBranch.Location = New System.Drawing.Point(560, 71)
        Me.TxtWorkinBranch.MaxLength = 35
        Me.TxtWorkinBranch.Name = "TxtWorkinBranch"
        Me.TxtWorkinBranch.Size = New System.Drawing.Size(318, 18)
        Me.TxtWorkinBranch.TabIndex = 912
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(461, 71)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(98, 16)
        Me.Label9.TabIndex = 914
        Me.Label9.Text = "Work in Branch"
        '
        'TxtWorkDivision
        '
        Me.TxtWorkDivision.AgAllowUserToEnableMasterHelp = False
        Me.TxtWorkDivision.AgLastValueTag = Nothing
        Me.TxtWorkDivision.AgLastValueText = Nothing
        Me.TxtWorkDivision.AgMandatory = False
        Me.TxtWorkDivision.AgMasterHelp = False
        Me.TxtWorkDivision.AgNumberLeftPlaces = 0
        Me.TxtWorkDivision.AgNumberNegetiveAllow = False
        Me.TxtWorkDivision.AgNumberRightPlaces = 0
        Me.TxtWorkDivision.AgPickFromLastValue = False
        Me.TxtWorkDivision.AgRowFilter = ""
        Me.TxtWorkDivision.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtWorkDivision.AgSelectedValue = Nothing
        Me.TxtWorkDivision.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtWorkDivision.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtWorkDivision.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtWorkDivision.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtWorkDivision.Location = New System.Drawing.Point(560, 51)
        Me.TxtWorkDivision.MaxLength = 35
        Me.TxtWorkDivision.Name = "TxtWorkDivision"
        Me.TxtWorkDivision.Size = New System.Drawing.Size(318, 18)
        Me.TxtWorkDivision.TabIndex = 911
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(461, 51)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(101, 16)
        Me.Label8.TabIndex = 913
        Me.Label8.Text = "Work in Division"
        '
        'FrmParty
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(907, 422)
        Me.Controls.Add(Me.TxtWorkinBranch)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.TxtWorkDivision)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtCurrency)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtPartyType)
        Me.Controls.Add(Me.LblPartyType)
        Me.Controls.Add(Me.TxtPinNo)
        Me.Controls.Add(Me.LblPinNo)
        Me.Controls.Add(Me.TxtUnderSubCode)
        Me.Controls.Add(Me.LblUnderSubCode)
        Me.Controls.Add(Me.TxtCostCenter)
        Me.Controls.Add(Me.LblCostCenter)
        Me.Controls.Add(Me.TxtContactPerson)
        Me.Controls.Add(Me.LblContactPerson)
        Me.Controls.Add(Me.TxtStRegNo)
        Me.Controls.Add(Me.LblStRegNo)
        Me.Controls.Add(Me.TxtPanNo)
        Me.Controls.Add(Me.LblPanNo)
        Me.Controls.Add(Me.TxtTinNo)
        Me.Controls.Add(Me.LblTinNo)
        Me.Controls.Add(Me.TxtCSTNo)
        Me.Controls.Add(Me.LblCSTNo)
        Me.Controls.Add(Me.TxtSalesTaxGroup)
        Me.Controls.Add(Me.LblSalesTaxGroup)
        Me.Controls.Add(Me.TxtFax)
        Me.Controls.Add(Me.LblBuyerFax)
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
        Me.Name = "FrmParty"
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
        Me.Controls.SetChildIndex(Me.LblBuyerFax, 0)
        Me.Controls.SetChildIndex(Me.TxtFax, 0)
        Me.Controls.SetChildIndex(Me.LblSalesTaxGroup, 0)
        Me.Controls.SetChildIndex(Me.TxtSalesTaxGroup, 0)
        Me.Controls.SetChildIndex(Me.LblCSTNo, 0)
        Me.Controls.SetChildIndex(Me.TxtCSTNo, 0)
        Me.Controls.SetChildIndex(Me.LblTinNo, 0)
        Me.Controls.SetChildIndex(Me.TxtTinNo, 0)
        Me.Controls.SetChildIndex(Me.LblPanNo, 0)
        Me.Controls.SetChildIndex(Me.TxtPanNo, 0)
        Me.Controls.SetChildIndex(Me.LblStRegNo, 0)
        Me.Controls.SetChildIndex(Me.TxtStRegNo, 0)
        Me.Controls.SetChildIndex(Me.LblContactPerson, 0)
        Me.Controls.SetChildIndex(Me.TxtContactPerson, 0)
        Me.Controls.SetChildIndex(Me.LblCostCenter, 0)
        Me.Controls.SetChildIndex(Me.TxtCostCenter, 0)
        Me.Controls.SetChildIndex(Me.LblUnderSubCode, 0)
        Me.Controls.SetChildIndex(Me.TxtUnderSubCode, 0)
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
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.TxtCurrency, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.Label3, 0)
        Me.Controls.SetChildIndex(Me.LinkLabel1, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.Label8, 0)
        Me.Controls.SetChildIndex(Me.TxtWorkDivision, 0)
        Me.Controls.SetChildIndex(Me.Label9, 0)
        Me.Controls.SetChildIndex(Me.TxtWorkinBranch, 0)
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
    Protected WithEvents TxtFax As AgControls.AgTextBox
    Protected WithEvents LblBuyerFax As System.Windows.Forms.Label
    Protected WithEvents TxtPhone As AgControls.AgTextBox
    Protected WithEvents LblPhone As System.Windows.Forms.Label
    Protected WithEvents TxtSalesTaxGroup As AgControls.AgTextBox
    Protected WithEvents LblSalesTaxGroup As System.Windows.Forms.Label
    Protected WithEvents TxtCSTNo As AgControls.AgTextBox
    Protected WithEvents LblCSTNo As System.Windows.Forms.Label
    Protected WithEvents TxtTinNo As AgControls.AgTextBox
    Protected WithEvents LblTinNo As System.Windows.Forms.Label
    Protected WithEvents TxtPanNo As AgControls.AgTextBox
    Protected WithEvents LblPanNo As System.Windows.Forms.Label
    Protected WithEvents TxtStRegNo As AgControls.AgTextBox
    Protected WithEvents LblStRegNo As System.Windows.Forms.Label
    Protected WithEvents TxtContactPerson As AgControls.AgTextBox
    Protected WithEvents LblContactPerson As System.Windows.Forms.Label
    Protected WithEvents TxtCostCenter As AgControls.AgTextBox
    Protected WithEvents LblCostCenter As System.Windows.Forms.Label
    Protected WithEvents TxtUnderSubCode As AgControls.AgTextBox
    Protected WithEvents LblUnderSubCode As System.Windows.Forms.Label
    Protected WithEvents TxtPinNo As AgControls.AgTextBox
    Protected WithEvents LblPinNo As System.Windows.Forms.Label
    Protected WithEvents TxtPartyType As AgControls.AgTextBox
    Protected WithEvents LblPartyType As System.Windows.Forms.Label
    Protected WithEvents LblAcGroupReq As System.Windows.Forms.Label
#End Region

    Private Sub FrmShade_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        AgL.PubFindQry = " SELECT H.SubCode AS SearchCode,  H.DispName AS [Display Name],AG.GroupName AS [Under Group], " & _
                        " H.ManualCode AS [Manual Code], H.Add1, H.Add2, H.Add3, C.CityName AS [City Name], " & _
                        " H.Mobile, H.EMail,  " & _
                        " H.EntryBy AS [Entry By], H.EntryDate AS [Entry Date], H.EntryType AS [Entry Type], " & _
                        " H.Status, D.Div_Name AS Division,SM.Name AS [Site Name] " & _
                        " FROM SubGroup H " & _
                        " LEFT JOIN Division D ON D.Div_Code=H.Div_Code  " & _
                        " LEFT JOIN SiteMast SM ON SM.Code=H.Site_Code " & _
                        " LEFT JOIN AcGroup AG ON AG.GroupCode = H.GroupCode " & _
                        " LEFT JOIN City C ON C.CityCode = H.CityCode  " & _
                        " WHERE MasterType = '" & mMasterType & "' "
        AgL.PubFindQryOrdBy = "[Name]"
    End Sub

    Private Sub FrmShade_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "SubGroup"
        MainLineTableCsv = "JobWorkerProcess"
        LogTableName = "SubGroup_Log"
        LogLineTableCsv = "JobWorkerProcess_Log"

        PrimaryField = "SubCode"
        AgL.GridDesign(Dgl1)
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList
        mQry = "Select S.SubCode as Code, S.ManualCode, S.DispName as Name " & _
                " From SubGroup S  " & _
                " Order By S.ManualCode "
        TxtManualCode.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)

        mQry = "Select S.SubCode as Code, S.DispName As Name " & _
                " From SubGroup S " & _
                " Order By S.DispName "
        TxtDispName.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)

        mQry = "SELECT C.CityCode AS Code, C.CityName, C.State " & _
                " FROM City C  "
        TxtCity.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

        mQry = "SELECT A.GroupCode AS Code, A.GroupName AS Name, A.GroupNature , A.Nature  " & _
                  " FROM AcGroup A "
        TxtAcGroup.AgHelpDataSet(2) = AgL.FillData(mQry, AgL.GCn)

        mQry = " Select Sg.SubCode As Code, Sg.DispName As Name From SubGroup Sg  "
        TxtUnderSubCode.AgHelpDataSet(0) = AgL.FillData(mQry, AgL.GCn)

        mQry = " SELECT H.Party_Type AS Code, H.Description FROM SubGroupType H  "
        TxtPartyType.AgHelpDataSet(0) = AgL.FillData(mQry, AgL.GCn)

        mQry = " SELECT H.Code, H.Name FROM CostCenterMast H "
        TxtCostCenter.AgHelpDataSet(0) = AgL.FillData(mQry, AgL.GCn)

        mQry = " SELECT Description AS Code, Description  FROM PostingGroupSalesTaxParty "
        TxtSalesTaxGroup.AgHelpDataSet(0) = AgL.FillData(mQry, AgL.GCn)

        mQry = " SELECT C.Code, C.Description  FROM Currency C "
        TxtCurrency.AgHelpDataSet(0) = AgL.FillData(mQry, AgL.GCn)

        mQry = " Select H.NCat As Code, H.Description As Process " & _
                " From Process H "
        Dgl1.AgHelpDataSet(Col1Process) = AgL.FillData(mQry, AgL.GCn)

    End Sub

    Private Sub FrmShade_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        mQry = "Select S.SubCode As SearchCode " & _
            " From SubGroup S  " & _
            " WHERE IfNull(S.IsDeleted,0)=0  And MasterType = '" & mMasterType & "'  "

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub TempAgent_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded
        sender(ColSNo, e.RowIndex).Value = e.RowIndex + 1
    End Sub

    Private Sub FrmQuality1_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DsTemp As DataSet
        Dim DrTemp As DataRow() = Nothing
        Dim I As Integer = 0

        mQry = "Select S.* " & _
                    " From SubGroup S  " & _
                    " Where S.SubCode='" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                mInternalCode = AgL.XNull(.Rows(0)("SubCode"))
                TxtManualCode.Text = AgL.XNull(.Rows(0)("ManualCode"))
                TxtDispName.Text = AgL.XNull(.Rows(0)("DispName"))
                TxtAcGroup.AgSelectedValue = AgL.XNull(.Rows(0)("GroupCode"))
                TxtAdd1.Text = AgL.XNull(.Rows(0)("Add1"))
                TxtAdd2.Text = AgL.XNull(.Rows(0)("Add2"))
                TxtCity.AgSelectedValue = AgL.XNull(.Rows(0)("CityCode"))
                TxtMobile.Text = AgL.XNull(.Rows(0)("Mobile"))
                TxtCreditDays.Text = AgL.XNull(.Rows(0)("CreditDays"))
                TxtCreditLimit.Text = AgL.XNull(.Rows(0)("CreditLimit"))
                TxtEMail.Text = AgL.XNull(.Rows(0)("EMail"))
                mNature = AgL.XNull(.Rows(0)("Nature"))
                mGroupNature = AgL.XNull(.Rows(0)("GroupNature"))

                TxtPinNo.Text = AgL.XNull(.Rows(0)("PIN"))
                TxtPhone.Text = AgL.XNull(.Rows(0)("Phone"))
                TxtFax.Text = AgL.XNull(.Rows(0)("Fax"))
                TxtCSTNo.Text = AgL.XNull(.Rows(0)("CstNo"))
                TxtTinNo.Text = AgL.XNull(.Rows(0)("TinNo"))
                TxtPanNo.Text = AgL.XNull(.Rows(0)("PAN"))
                TxtStRegNo.Text = AgL.XNull(.Rows(0)("STRegNo"))
                TxtContactPerson.Text = AgL.XNull(.Rows(0)("ContactPerson"))
                TxtSalesTaxGroup.AgSelectedValue = AgL.XNull(.Rows(0)("SalesTaxPostingGroup"))
                TxtCurrency.AgSelectedValue = AgL.XNull(.Rows(0)("Currency"))
                TxtCostCenter.AgSelectedValue = AgL.XNull(.Rows(0)("CostCenter"))
                TxtPartyType.AgSelectedValue = AgL.XNull(.Rows(0)("Party_Type"))
                TxtUnderSubCode.AgSelectedValue = AgL.XNull(.Rows(0)("Parent"))

                TxtWorkDivision.Tag = AgL.XNull(.Rows(0)("DivisionList"))
                TxtWorkDivision.Text = RetFilterValue(TxtWorkDivision.Tag, "SELECT Div_Name FROM Division", "Div_Code")
                TxtWorkinBranch.Tag = AgL.XNull(.Rows(0)("SiteList"))
                TxtWorkinBranch.Text = RetFilterValue(TxtWorkinBranch.Tag, "SELECT Name  FROM SiteMast", "Code")

            End If
        End With

        mQry = "Select * From JobWorkerProcess where SubCode = '" & SearchCode & "' Order By Sr "
        DsTemp = AgL.FillData(mQry, AgL.GCn)
        With DsTemp.Tables(0)
            Dgl1.RowCount = 1
            Dgl1.Rows.Clear()
            If .Rows.Count > 0 Then
                For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                    Dgl1.Rows.Add()
                    Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                    Dgl1.AgSelectedValue(Col1Process, I) = AgL.XNull(.Rows(I)("Process"))
                Next I
            End If
        End With

        Topctrl1.tPrn = False
    End Sub

    Private Function RetFilterValue(ByVal bStrFilterValue As String, ByVal bStrQry As String, ByVal bCond As String) As String
        RetFilterValue = ""
        Dim mItemStr As String = ""
        If bStrFilterValue <> "" Then
            mQry = Replace(bStrQry, " FROM", " + ', ' FROM") & " Where " & bCond & " IN ( " & Replace(bStrFilterValue, "|", "'") & " ) " & _
                    " FOR XML Path ('') "
            mItemStr = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)
            mItemStr = mItemStr.Substring(0, Len(mItemStr) - 2)
        End If
        RetFilterValue = mItemStr
    End Function

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
        Dim bName$ = ""
        Dim I As Integer = 0
        Dim mSr As Integer = 0

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
                mQry = "Select count(*) From SubGroup Where ManualCode='" & TxtManualCode.Text & "' And IfNull(IsDeleted,0)=0 "
                If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Code Already Exists")
            Else
                mQry = "Select count(*) From SubGroup Where ManualCode ='" & TxtManualCode.Text & "' And SubCode<>'" & mInternalCode & "'  And IfNull(IsDeleted,0)=0 "
                If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Code Already Exists")
            End If

            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = True

            bName = TxtDispName.Text + " {" + TxtManualCode.Text + "}"
            If TxtUnderSubCode.Tag = "" Then TxtUnderSubCode.Tag = mSearchCode

            If Topctrl1.Mode = "Add" Then
                mQry = "INSERT INTO SubGroup(SubCode, Site_Code, Name, DispName, " & _
                        " GroupCode, GroupNature, MasterType,	ManualCode,	Nature,	Add1,	Add2,	CityCode,  " & _
                        " PIN, Phone, FAX, CSTNo, TINNo, PAN, STRegNo, ContactPerson, CostCenter, Party_Type, " & _
                        " Mobile, CreditDays, CreditLimit, EMail, Parent, SalesTaxPostingGroup, Currency, DivisionList, SiteList, " & _
                        " EntryBy, EntryDate,  EntryType, EntryStatus, Div_Code, Status, " & _
                        " U_Name, U_EntDt, U_AE) " & _
                        " VALUES(" & AgL.Chk_Text(mSearchCode) & ", " & _
                        " '" & AgL.PubSiteCode & "', " & AgL.Chk_Text(bName) & ",	" & _
                        " " & AgL.Chk_Text(TxtDispName.Text) & ", " & AgL.Chk_Text(TxtAcGroup.AgSelectedValue) & ", " & _
                        " " & AgL.Chk_Text(mGroupNature) & ", " & AgL.Chk_Text(mMasterType) & ", " & AgL.Chk_Text(TxtManualCode.Text) & ", " & _
                        " " & AgL.Chk_Text(mNature) & ", " & AgL.Chk_Text(TxtAdd1.Text) & ", " & _
                        " " & AgL.Chk_Text(TxtAdd2.Text) & ", " & _
                        " " & AgL.Chk_Text(TxtCity.AgSelectedValue) & ", " & _
                        " " & AgL.Chk_Text(TxtPinNo.Text) & ", " & AgL.Chk_Text(TxtPhone.Text) & ", " & AgL.Chk_Text(TxtFax.Text) & ", " & _
                        " " & AgL.Chk_Text(TxtCSTNo.Text) & ", " & AgL.Chk_Text(TxtTinNo.Text) & ", " & AgL.Chk_Text(TxtPanNo.Text) & ", " & _
                        " " & AgL.Chk_Text(TxtStRegNo.Text) & ", " & AgL.Chk_Text(TxtContactPerson.Text) & ", " & _
                        " " & AgL.Chk_Text(TxtCostCenter.AgSelectedValue) & ", " & _
                        " " & AgL.Chk_Text(TxtPartyType.AgSelectedValue) & ", " & _
                        " " & AgL.Chk_Text(TxtMobile.Text) & ", " & _
                        " " & Val(TxtCreditDays.Text) & ", " & _
                        " " & Val(TxtCreditLimit.Text) & ", " & _
                        " " & AgL.Chk_Text(TxtEMail.Text) & ", " & _
                        " " & AgL.Chk_Text(TxtUnderSubCode.AgSelectedValue) & ", " & _
                        " " & AgL.Chk_Text(TxtSalesTaxGroup.AgSelectedValue) & ", " & _
                        " " & AgL.Chk_Text(TxtCurrency.AgSelectedValue) & ", " & _
                        " '" & RetStringValue(TxtWorkDivision) & "', '" & RetStringValue(TxtWorkinBranch) & "', " & _
                        " " & AgL.Chk_Text(AgL.PubUserName) & ", " & AgL.Chk_Text(AgL.GetDateTime(AgL.GcnRead)) & ", " & _
                        " " & AgL.Chk_Text(Topctrl1.Mode) & ", " & AgL.Chk_Text(LogStatus.LogOpen) & ", " & _
                        " " & AgL.Chk_Text(TxtDivision.AgSelectedValue) & ", " & AgL.Chk_Text(TxtStatus.Text) & ", " & _
                        " '" & AgL.PubUserName & "','" & Format(AgL.PubLoginDate, "Short Date") & "', 'A') "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            Else
                mQry = "UPDATE SubGroup " & _
                        " SET " & _
                        " Name = " & AgL.Chk_Text(bName) & ", " & _
                        " DispName = " & AgL.Chk_Text(TxtDispName.Text) & ", " & _
                        " GroupCode = " & AgL.Chk_Text(TxtAcGroup.AgSelectedValue) & ", " & _
                        " GroupNature = " & AgL.Chk_Text(mGroupNature) & ", " & _
                        " MasterType = " & AgL.Chk_Text(mMasterType) & ", " & _
                        " ManualCode = " & AgL.Chk_Text(TxtManualCode.Text) & ", " & _
                        " Nature = " & AgL.Chk_Text(mNature) & ", " & _
                        " Add1 = " & AgL.Chk_Text(TxtAdd1.Text) & ", " & _
                        " Add2 = " & AgL.Chk_Text(TxtAdd2.Text) & ", " & _
                        " CityCode = " & AgL.Chk_Text(TxtCity.AgSelectedValue) & ", " & _
                        " Mobile = " & AgL.Chk_Text(TxtMobile.Text) & ", " & _
                        " CreditDays = " & Val(TxtCreditDays.Text) & ", " & _
                        " CreditLimit = " & Val(TxtCreditLimit.Text) & ", " & _
                        " EMail = " & AgL.Chk_Text(TxtEMail.Text) & ", " & _
                        " PIN = " & AgL.Chk_Text(TxtPinNo.Text) & ", " & _
                        " Phone = " & AgL.Chk_Text(TxtPhone.Text) & ", " & _
                        " FAX = " & AgL.Chk_Text(TxtFax.Text) & ", " & _
                        " CSTNo = " & AgL.Chk_Text(TxtCSTNo.Text) & ", " & _
                        " TINNo = " & AgL.Chk_Text(TxtTinNo.Text) & ", " & _
                        " PAN = " & AgL.Chk_Text(TxtPanNo.Text) & ", " & _
                        " STRegNo = " & AgL.Chk_Text(TxtStRegNo.Text) & ", " & _
                        " ContactPerson = " & AgL.Chk_Text(TxtContactPerson.Text) & ", " & _
                        " CostCenter = " & AgL.Chk_Text(TxtCostCenter.AgSelectedValue) & ", " & _
                        " Party_Type = " & AgL.Chk_Text(TxtPartyType.AgSelectedValue) & ", " & _
                        " Parent = " & AgL.Chk_Text(TxtUnderSubCode.AgSelectedValue) & ", " & _
                        " SalesTaxPostingGroup = " & AgL.Chk_Text(TxtSalesTaxGroup.AgSelectedValue) & ", " & _
                        " Currency = " & AgL.Chk_Text(TxtCurrency.AgSelectedValue) & ", " & _
                        " DivisionList = '" & RetStringValue(TxtWorkDivision) & "', " & _
                        " SiteList = '" & RetStringValue(TxtWorkinBranch) & "', " & _
                        " EntryBy = " & AgL.Chk_Text(AgL.PubUserName) & ", " & _
                        " EntryDate = " & AgL.Chk_Text(AgL.GetDateTime(AgL.GcnRead)) & ", " & _
                        " EntryType = " & AgL.Chk_Text(Topctrl1.Mode) & ", " & _
                        " EntryStatus = " & AgL.Chk_Text(LogStatus.LogOpen) & ", " & _
                        " Div_Code = " & AgL.Chk_Text(TxtDivision.AgSelectedValue) & ", " & _
                        " U_AE = 'E', " & _
                        " Edit_Date = '" & Format(AgL.PubLoginDate, "Short Date") & "', " & _
                        " ModifiedBy = '" & AgL.PubUserName & "' " & _
                        " Where Subcode = " & AgL.Chk_Text(mSearchCode) & "  "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If

            mQry = "Delete From JobWorkerProcess Where SubCode  = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            With Dgl1
                For I = 0 To .RowCount - 1
                    If .Item(Col1Process, I).Value <> "" Then
                        mSr += 1
                        mQry = " INSERT INTO JobWorkerProcess(Subcode, Sr, Process) " & _
                                " VALUES (" & AgL.Chk_Text(mSearchCode) & ", " & mSr & " , " & _
                                " " & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1Process, I)) & ") "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    End If
                Next
            End With

            Call AgL.LogTableEntry(mSearchCode, Me.Text, AgL.MidStr(Topctrl1.Mode, 0, 1), AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd)

            AgL.ETrans.Commit()
            mTrans = False


            If AgL.PubMoveRecApplicable Then
                FIniMaster(0, 1)
                Topctrl1_tbRef()
            End If

            TxtCurrency.AgLastValueTag = TxtCurrency.Tag
            TxtCurrency.AgLastValueText = TxtCurrency.Text

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

    Private Function RetStringValue(ByVal ObjTextBox As TextBox) As String
        RetStringValue = ""
        If ObjTextBox.Tag <> "" Then
            RetStringValue = Replace(ObjTextBox.Tag, "'", "|")
        End If
    End Function

    Private Sub TxtCity_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtCity.Enter
        Select Case sender.name
            Case TxtCity.Name

        End Select
    End Sub

    Private Sub Control_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtCity.Validating, TxtAcGroup.Validating, TxtCity.Validating
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
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmSteward_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsReturnValue = False Then
            AgL.WinSetting(Me, 450, 913, 0, 0)
        Else
            Topctrl1.FButtonClick(0)
        End If
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub TxtDescription_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtCreditLimit.KeyDown
        'If e.KeyCode = Keys.Enter Then
        '    If MsgBox("Do you want to save?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Save") = MsgBoxResult.Yes Then
        '        Topctrl1.FButtonClick(13)
        '    End If
        'End If
    End Sub

    Private Sub FrmParty_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        TxtCurrency.Tag = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultCurrency"))
        TxtManualCode.Text = AgL.XNull(AgL.Dman_Execute("Select IfNull(Max(Convert(Numeric,I.ManualCode)),0)+1 AS ManualCode FROM SubGroup I  WHERE isnumeric(I.ManualCode)>0", AgL.GCn).ExecuteScalar)
        If TxtCurrency.Tag <> "" Then
            TxtCurrency.Text = AgL.XNull(AgL.Dman_Execute("Select Description From Currency Where Code = '" & TxtCurrency.Tag & "'  ", AgL.GCn).ExecuteScalar)
        End If
        TxtSalesTaxGroup.Tag = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultSalesTaxGroupParty"))
        TxtSalesTaxGroup.Text = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultSalesTaxGroupParty"))

        If mSubGroupNature = ESubgroupNature.Customer Then
            TxtAcGroup.AgSelectedValue = SubGroupConst.GroupCode_Debtors
            mNature = SubGroupConst.Nature_Debtors
            mGroupNature = SubGroupConst.GroupNature_Debtors
        Else
            TxtAcGroup.AgSelectedValue = SubGroupConst.GroupCode_Creditors
            mNature = SubGroupConst.Nature_Creditors
            mGroupNature = SubGroupConst.GroupNature_Creditors
        End If

        TxtCurrency.Tag = TxtCurrency.AgLastValueTag
        TxtCurrency.Text = TxtCurrency.AgLastValueText

        TxtManualCode.Focus()
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub FrmParty_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
        TxtManualCode.Focus()
    End Sub

    Private Sub FrmParty_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 35, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Process, 250, 50, Col1Process, True, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.Anchor = AnchorStyles.None
    End Sub

    Private Sub TxtWorkDivision_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtWorkDivision.KeyDown, TxtWorkinBranch.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then Exit Sub
            If e.KeyCode = Keys.Delete Then sender.Tag = "" : sender.Text = "" : Exit Sub
            Select Case sender.Name
                Case TxtWorkDivision.Name
                    If TxtWorkDivision.AgHelpDataSet Is Nothing Then
                        FHPGD_TxtFilter(TxtWorkDivision, "SELECT 'o' AS Tick, Div_Code, Div_Name FROM Division Order By Div_Name")
                    End If

                Case TxtWorkinBranch.Name
                    If TxtWorkinBranch.AgHelpDataSet Is Nothing Then
                        FHPGD_TxtFilter(TxtWorkinBranch, "SELECT 'o' AS Tick,  Code, Name FROM SiteMast Order By Name")
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FHPGD_TxtFilter(ByVal Text As TextBox, ByVal mQry As String)
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(AgL.FillData(mQry, AgL.GCn).TABLES(0)), "", 400, 370, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Name", 250, DataGridViewContentAlignment.MiddleLeft)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            Text.Tag = FRH_Multiple.FFetchData(1, "'", "'", ",", True)
            Text.Text = FRH_Multiple.FFetchData(2, "", "", ",", True)
        End If

        FRH_Multiple = Nothing
    End Sub
End Class

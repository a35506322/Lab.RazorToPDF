namespace ReazorTemplate.Models;

public class LoanViewModel
{
    // 訂單編號
    public string OrderNumber { get; set; }

    // 申請人基本資料
    public string ChineseName { get; set; } // 中文姓名
    public string IdentityNumber { get; set; } // 身分證字號
    public string BirthYear { get; set; } // 出生日期-年
    public string BirthMonth { get; set; } // 出生日期-月
    public string BirthDay { get; set; } // 出生日期-日
    public string IdentityIssueYear { get; set; } // 身分證換補領日期-年
    public string IdentityIssueMonth { get; set; } // 身分證換補領日期-月
    public string IdentityIssueDay { get; set; } // 身分證換補領日期-日
    public int IdentityIssueType { get; set; } // 身分證發證類型 (1.初發 2.補發 3.換發)
    public string IssueLocation { get; set; } // 發證地
    public string BirthCountry { get; set; } // 出生地-國家
    public string BirthCity { get; set; } // 出生地-城市

    // 戶籍地址
    public string RegisteredPostalCode { get; set; } // 郵遞區號
    public string RegisteredCounty { get; set; } // 縣市
    public string RegisteredTownship { get; set; } // 鄉鎮市區
    public string RegisteredVillage { get; set; } // 里村
    public string RegisteredRoad { get; set; } // 路街
    public string RegisteredLane { get; set; } // 巷
    public string RegisteredAlley { get; set; } // 弄
    public string RegisteredNumber { get; set; } // 號
    public string RegisteredFloor { get; set; } // 樓

    // 居住地址
    public bool IsSameAsRegistered { get; set; } // 同戶籍地
    public string ResidentialPostalCode { get; set; }
    public string ResidentialCounty { get; set; }
    public string ResidentialTownship { get; set; }
    public string ResidentialVillage { get; set; }
    public string ResidentialRoad { get; set; }
    public string ResidentialLane { get; set; }
    public string ResidentialAlley { get; set; }
    public string ResidentialNumber { get; set; }
    public string ResidentialFloor { get; set; }

    // 帳單地址
    public bool BillingIsSameAsRegistered { get; set; } // 同戶籍地
    public bool BillingIsSameAsResidential { get; set; } // 同居住地
    public string BillingPostalCode { get; set; }
    public string BillingCounty { get; set; }
    public string BillingTownship { get; set; }
    public string BillingVillage { get; set; }
    public string BillingRoad { get; set; }
    public string BillingLane { get; set; }
    public string BillingAlley { get; set; }
    public string BillingNumber { get; set; }
    public string BillingFloor { get; set; }

    // 聯絡資訊
    public string ContactPhone { get; set; } // 聯絡電話
    public string MobilePhone { get; set; } // 行動電話
    public string Email { get; set; } // E-mail

    // 職業資料
    public string CompanyName { get; set; } // 公司名稱
    public string JobTitle { get; set; } // 職稱
    public string YearsOfService { get; set; } // 年資
    public int MonthlyIncome { get; set; } // 月收入
    public string Occupation { get; set; } // 職業別
    public string JobPosition { get; set; } // 職務別
    public string CompanyTaxId { get; set; } // 公司統編
    public string CompanyPhone { get; set; } // 公司電話

    // 公司地址
    public string CompanyAddress { get; set; } // 公司地址

    // 所得資金來源 (多選)
    public bool IncomeSourceBusiness { get; set; } // 經營事業收入
    public bool IncomeSourceSalary { get; set; } // 薪資所得
    public bool IncomeSourceInheritance { get; set; } // 繼承/贈與
    public bool IncomeSourceInvestment { get; set; } // 投資理財
    public bool IncomeSourceRetirement { get; set; } // 退休金
    public bool IncomeSourceRental { get; set; } // 租金收入
    public bool IncomeSourceProfession { get; set; } // 專案執業收入
    public bool IncomeSourceIdleFunds { get; set; } // 閒置資金
    public bool IncomeSourceOther { get; set; } // 其他
    public string IncomeSourceOtherDesc { get; set; } // 其他描述

    // 貸款資料
    public bool IsRevolvingLoan { get; set; } // 循環動用型
    public bool IsOneTimeLoan { get; set; } // 一次撥付型
    public int LoanAmount { get; set; } // 申請金額(萬元)
    public int LoanTerm { get; set; } // 申請年限(1~7年)

    // 匯款資訊
    public string BankName { get; set; } // 匯入銀行
    public string BankAccountNumber { get; set; } // 匯入帳號

    // 代償資料 (多筆)
    public string CompensationBankName_1 { get; set; } // 代償銀行名稱1
    public string CompensationBranchName_1 { get; set; } // 分行名稱1
    public string CompensationAccountNumber_1 { get; set; } // 帳號1
    public string CompensationAccountName_1 { get; set; } // 戶名1
    public int CompensationAmount_1 { get; set; } // 申請金額1
    public int CompensationActualAmount_1 { get; set; } // 實際代償金額1

    public string CompensationBankName_2 { get; set; } // 代償銀行名稱2
    public string CompensationBranchName_2 { get; set; } // 分行名稱2
    public string CompensationAccountNumber_2 { get; set; } // 帳號2
    public string CompensationAccountName_2 { get; set; } // 戶名2
    public int CompensationAmount_2 { get; set; } // 申請金額2
    public int CompensationActualAmount_2 { get; set; } // 實際代償金額2

    public string CompensationBankName_3 { get; set; } // 代償銀行名稱3
    public string CompensationBranchName_3 { get; set; } // 分行名稱3
    public string CompensationAccountNumber_3 { get; set; } // 帳號3
    public string CompensationAccountName_3 { get; set; } // 戶名3
    public int CompensationAmount_3 { get; set; } // 申請金額3
    public int CompensationActualAmount_3 { get; set; } // 實際代償金額3

    // 申請資訊
    public string ApplyDate { get; set; } // 申請日期

    // 銀行內部使用
    public string DepartmentName { get; set; } // 單位別
    public string PromotionStaff { get; set; } // 推廣人員
    public string DepartmentCode { get; set; } // 單位別代碼
}

function clear_contactus() { $("#cname").val(""), $("#pnum").val(""), $("#email").val(""), $("#name").val(""), $("#msg").val("") } function checkvalidemail(a) { var e, t = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i; return e = t.test($(a).val().trim()) ? !0 : !1 } $(document).ready(function () { $("#down").on("click", function (a) { window.location.href = "http://admin.mobiocean.com/API.aspx", $("#myModaldownload").modal("toggle") }), $(".demoreq").click(function () { $("#myModal").modal("hide"); var a = "http://admin.mobiocean.com/api/Newssubscription/NewsSub", e = /\S+@\S+\.\S+/, t = $("input#pdemovid").val().trim(); return "" == t ? (alert("Please enter Email"), $("input#pdemovid").focus(), !1) : e.test(t) ? ($.ajax({ url: a, type: "POST", data: { IsSubscription: 0, EmailId: t }, success: function (a, e, t) { $("input#pdemovid").val(""), $("#myModal3").modal("hide"), alert(a), $(".pdemo").click() }, error: function (a, e, t) { $("#myModal3").modal("hide"), alert("error") } }), !1) : (alert("Please enter a valid email address."), !1) }), $("#askme").click(function () { var a = $("#name").val().trim(), e = $("#cname").val().trim(), t = $("#pnum").val().trim(), i = $("#email").val().trim(), l = $("#msg").val().trim(), r = /^\d{10}$/, n = "http://admin.mobiocean.com/api/ContactUs/InsertIntoContactUs"; return "" != e && "" != t && "" != l && "" != a && "" != i ? t.match(r) ? checkvalidemail("#email") ? ($.ajax({ url: n, type: "POST", data: { Name: a, MobileNo: t, EmailId: i, Company_Name: e, TypeOfIndustry: e, Country: e, Remark: l }, success: function (a, e, t) { 0 != a ? (clear_contactus(), $("#myModal").modal("hide"), alert(a)) : alert("Sorry something went wrong! Please try again later.") }, error: function (a, e, t) { alert("error") } }), !1) : (alert("Enter valid email"), $("input#email").focus(), !1) : (alert("Enter valid mobile number"), $("input#pnum").focus(), !1) : (alert("Fill all required field"), !1) }), $(".subscriptionbtn").click(function () { alert(""); var a = "http://admin.mobiocean.com/api/Newssubscription/NewsSub", e = /\S+@\S+\.\S+/, t = $("input#subEmailId").val().trim(); return "" == t ? (alert("Please enter Email"), $("input#subEmailId").focus(), !1) : e.test(t) ? ($.ajax({ url: a, type: "POST", data: { EmailId: t, IsSubscription: 1 }, success: function (a, e, t) { alert(a), $("input#subEmailId").val("") }, error: function (a, e, t) { alert("error") } }), !1) : (alert("Please enter a valid email address."), !1) }) });
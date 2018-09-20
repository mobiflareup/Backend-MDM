var RupPopUp = {
    totHeight: null,
    popWidth: null,
    topFrom: "230",
    callFunction: null,
    popupTitle: null,
    popupBorderColor: '#fea700',
    titleBackColor: '#6c88c5',
    popBackColor: '#fff',
    beforeClose: '',
    create: function(popWidth, callFunction, popupTitle, beforeClose, topFrom) {
        this.popWidth = popWidth;
        this.totHeight = RupPopUp.getTopHeight();
        this.callFunction = callFunction;
        this.popupTitle = popupTitle;

        // if (contType)
            // this.contType = contType;

        if (beforeClose)
            this.beforeClose = beforeClose;

        if (topFrom)
            this.topFrom = topFrom;

        //Add Shadow
        RupPopUp.addShadow();

        //Add PopUp
        RupPopUp.addPopUp();
    },
    getTopHeight: function() {
        if (document.body) {
            var totalheight = Math.max(
				Math.max(document.body.scrollHeight, document.documentElement.scrollHeight),
				Math.max(document.body.offsetHeight, document.documentElement.offsetHeight),
				Math.max(document.body.clientHeight, document.documentElement.clientHeight)
			);
        } else {
            var totalheight = Math.max(document.documentElement.scrollHeight, document.documentElement.offsetHeight, document.documentElement.clientHeight);
        }
        return totalheight;
    },
    addShadow: function() {
        var objShadow = document.getElementById('RupPopUp_Shadow');
        if (!objShadow) {
			var objDv = document.createElement('div');
			objDv.id = "RupPopUp_Shadow";
			objDv.className = "RupPopUp_Shadow";
			objDv.setAttribute("onclick", "RupPopUp.closePopUp();");
			objDv.setAttribute("style", "height:" + $(document).height() + "px");
			document.body.appendChild(objDv);
		}
    },
    addPopUp: function() {
        var objPopup = document.getElementById('RupPopUp_Popup');
        if (!objPopup) {
            var objDv = document.createElement('div');
            objDv.id = "RupPopUp_Popup";
            objDv.className = "RupPopUp_Popup";
            objDv.setAttribute("style", "top:" + this.topFrom + "px");
            var str =
			'<div class="RupPopUp_Popup_Inner" style="width:' + this.popWidth + 'px;background:' + this.popupBorderColor + ';">'
				+ '<h1 style="background:' + this.titleBackColor + '; font-family:MyriadProRegular,arial; padding:2%; color:#fff; font-size: 26px;">'
					+ '<span>' + this.popupTitle + '</span>'
					+ '<a class="RupPopUp_Popup_Close" onclick="' + this.beforeClose + ';RupPopUp.closePopUp();" href="javascript:;"><b><img src="'+base_url+'www/images/closebtn.png"  alt="close" /></b></a>'
				+ '</h1>'
				+ '<div class="RupPopUp_Popup_Main" id="RupPopUp_Popup_Main" style="background:' + this.popBackColor + ';">'
					+ '<div>'
						+ '<center><img src="'+base_url+'www/image/loader.gif" /></center>'
					+ '</div>'
					+ '<div class="clear"></div>'
				+ '</div>'
				+ '<div class="clear"></div>'
			+ '</div>';
            objDv.innerHTML = str;
            document.body.appendChild(objDv);
        }
    },
    closePopUp: function() {
        var objShadow = document.getElementById('RupPopUp_Shadow');
        var objPopup = document.getElementById('RupPopUp_Popup');

        objShadow.parentNode.removeChild(objShadow);
        objPopup.parentNode.removeChild(objPopup);
    }
};
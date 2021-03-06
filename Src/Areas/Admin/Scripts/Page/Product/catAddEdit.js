﻿$(document).ready(() => {
    // #region General
    $(".styled").uniform({
        radioClass: 'choice'
    });
    $('.maxlength').maxlength({
        placement: 'bottom-left'
    });

    let CatID = $("#FrmCat #ID");

    //tab switch
    $(".tabbable .nav-tabs li a").on("click", (e) => {
        let t = e.currentTarget,
            isActive = $(t).parent().hasClass("active"),
            dataTab = $(t).attr("data-tab"),
            id = CatID.val();

        if (!isActive) {
            if (id == "-1") {
                notifiction(2, "ابتدا مشخصات دسته را ثبت نمائید.");
            }
            else {
                swichTab(t);
                btnSaveOnOff();
                switch (dataTab) {
                    case "#Details":
                        btnSaveOnOff();
                        break;
                    case "#Imgs":
                        ImgList();
                        break;
                    case "#PCPGs":
                        PropList();
                }
            }
        }
    });
    //#endregion

    //#region CatAddEdit
    let frmCat = $("#FrmCat");

    function saveFrmCat(e) {
        let isFormValid = frmCat.valid(),
            targetId = $(e.currentTarget).attr("id");

        if (isFormValid) {
            btnSaveOnOff();
            startAnimate(".tab-content");

            let model = {
                ID: CatID.val(),
                PID: $("#FrmCat #PID[checked='checked']").val(),
                Title: $("#FrmCat #Title").val(),
                EnTitle: $("#FrmCat #EnTitle").val()
            },
                url = getUrl("Product/AddEditCat");

            $.ajax({
                url: url,
                type: 'POST',
                data: JSON.stringify(model),
                processData: false,
                contentType: "application/json",
                success: function (Result) {
                    if (Result.Message == "Success") {
                        CatID.val(Result.Data);
                        finishAnimate(".tab-content");
                        btnSaveOnOff();
                        notifiction(0, "عمیات با موفقیت انجام شد.");
                        if (targetId == "btnSaveNext") {
                            pageLoad('/Admin/Product/Category', 'دستبندی محصولات', 'procCat');
                        }
                    } else {
                        notifiction(1, Result.Message);
                        finishAnimate(".tab-content");
                    }
                },
                error: function (Result) {
                    finishAnimate(".tab-content");
                    notifiction(1, Result.Message);
                }
            });
        }
    }

    frmCat.delegate("#PID", "change", (e) => {
        $("#FrmCat #PID[checked='checked']").removeAttr("checked");
        $(e.currentTarget).attr("checked", "checked");
    });

    $("#btnSave").on("click", (e) => {
        saveFrmCat(e);
    });
    $("#btnSaveNext").on("click", (e) => {
        saveFrmCat(e);
    });
    //#endregion

    //#region CatProp
    let CatPropList = {},
        modal = $("#addEditPCPG"),
        modalTitle = $("#addEditPCPG .modal-title"),
        PropID = $("#FrmPCPG #ID"),
        PropTitle = $("#FrmPCPG #Title"),
        PropEnTitle = $("#FrmPCPG #EnTitle"),
        PropPID = $("#FrmPCPG #TreeSelect"),
        frmProcCatMode = (id) => {
            let temp = (id == undefined || id == -1) ? "Add" : "Edit";
            return temp;
        };

    function PropList(pid) {
        if (pid == undefined) {
            let url = getUrl("Product/PCPGList/", `?catID=${CatID.val()}`);
            $.ajax({
                url: url,
                type: "Get",
                data: CatID.val(),
                success: (Result) => {
                    if (Result.Message == "Success") {
                        if (Result.Data.length > 0) {
                            NoRecords(false);
                            CatPropList = Result.Data;
                            let source = $('#PCPGSource').html(),
                                template = Handlebars.compile(source),
                                parentList = $.grep(CatPropList, (item) => {
                                    return (item.PID == null)
                                }),
                                list = template({ list: parentList });
                            $('#tblPCPG tbody').html(list);
                        }
                        else {
                            NoRecords(true);
                        }
                    }
                    else {
                        notifiction(1, Result.Message);
                    }
                },
                error: (Result) => {
                    notifiction(1, Result.Message);
                }
            });
        }
        else {
            let source = $('#PCPGSource').html(),
                template = Handlebars.compile(source),
                childList = $.grep(CatPropList, (item) => {
                    return (item.PID == pid);
                }),
                list = template({ list: childList });
            $('#' + pid + ' td table tbody').html(list);
        }
    };
    function getFrmCatProp(e) {
        let target = $(e.currentTarget),
            id = target.attr("data-id"),
            title = target.attr("data-title"),
            enTitle = target.attr("data-enTitle"),
            hasChild = target.attr("data-hasChild"),
            assignCount = target.attr("data-assignCount"),
            pid = target.attr("data-pid") == "" ? id : target.attr("data-pid");

        if (frmProcCatMode(id) == "Add") {
            PropID.val("-1");
            PropTitle.val("");
            PropEnTitle.val("");
            clearMultiSelect(PropPID, "PID");
            $.each(CatPropList, (i) => {
                fillSelectMulti(CatPropList, CatPropList[i], pid, PropPID, "PID", false);
            });
            modalTitle.html("افزودن ویژگی");
        }
        else {
            PropID.val(id);
            PropTitle.val(title);
            PropEnTitle.val(enTitle);
            if (hasChild == "false" && assignCount == 0) {
                $.each(CatPropList, (i) => {
                    fillSelectMulti(CatPropList, CatPropList[i], pid, PropPID, "PID", false);
                });
            }
            else {
                $.each(CatPropList, (i) => {
                    fillSelectMulti(CatPropList, CatPropList[i], pid, PropPID, "PID", true, true);
                });
            }
            modalTitle.html("ویرایش ویژگی");
        }
    };
    function AddEditCatProp() {
        let isTitleValid = validateInput(PropTitle, "لطفا یک مورد را انتخاب کنید."),
            isEnTitleValid = validateInput(PropEnTitle, "لطفا یک مورد را انتخاب کنید.");

        if (isTitleValid && isEnTitleValid) {
            startAnimate("#addEditPCPG .modal-content");

            let model = {
                ID: PropID.val(),
                PID: $("#FrmPCPG #PID[checked='checked']").val(),
                Title: PropTitle.val(),
                EnTitle: PropEnTitle.val(),
                CatID: CatID.val()
            },
                url = getUrl("Product/AddEditPCPG");

            $.ajax({
                url: url,
                type: 'POST',
                data: JSON.stringify(model),
                processData: false,
                contentType: "application/json",
                success: function (Result) {
                    if (Result.Message == "Success") {
                        finishAnimate("#addEditPCPG .modal-content");
                        notifiction(0, "عمیات با موفقیت انجام شد.");
                        modal.modal("hide");
                        PropList();
                    } else {
                        notifiction(1, Result.Message);
                        finishAnimate("#addEditPCPG .modal-content");
                    }
                },
                error: function (Result) {
                    finishAnimate("#addEditPCPG .modal-content");
                    notifiction(1, Result.Message);
                }
            });
        }
    };
    function delCatProp(id) {
        swal({
            title: "آیا از انجام این کار اطمینان دارید ؟",
            text: "شما قادر به بازیابی دوباره این رکورد نخواهید بود !",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#EF5350",
            confirmButtonText: "بله ، حذف شود",
            cancelButtonText: "خیر ، لغو شود",
            closeOnConfirm: true,
            closeOnCancel: true
        }, (isConfirm) => {
            if (isConfirm) {
                let url = getUrl("Product/DelPCPG");
                $.ajax({
                    url: url,
                    type: 'POST',
                    data: id,
                    processData: false,
                    contentType: "application/json",
                    success: function (Result) {
                        if (Result.Message == "Success") {
                            PropList();
                            swal({
                                title: "عملیات حذف انجام شد .",
                                text: "رکورد مد نظر شما با موفقیت حذف گردید .",
                                confirmButtonColor: "#66BB6A",
                                confirmButtonText: "باشه",
                                type: "success"
                            });
                        }
                        else {
                            notifiction(1, Result.Message);
                        }
                    },
                    error: function (Result) {
                        notifiction(1, Result.Message);
                    }
                });
            }
            else {
                swal({
                    title: "در خواست لغو شد.",
                    text: "رکورد شما در صحت کامل قرار دارد :)",
                    confirmButtonColor: "#66BB6A",
                    confirmButtonText: "باشه",
                    type: "success"
                });
            }
        });
    }

    $("#PCPGs").delegate("#getChild", "click", (e) => {
        let PID = $(e.currentTarget).attr('data-id');
        PropList(PID);
    });
    $("#PCPGs").delegate("#addEdit", "click", (e) => {
        getFrmCatProp(e);
    });
    $("#PCPGs").delegate("#del", "click", (e) => {
        let id = $(e.currentTarget).attr("data-id"),
            hasChild = $(e.currentTarget).attr("data-hasChild"),
            assignCount = $(e.currentTarget).attr("data-assignCount");
        if (hasChild = "false" && assignCount == 0) {
            delCatProp(id);
        } else {
            notifiction(2, "شما تنها می توانید ویژگی هایی که فرزند و اختصاصی ندارند را حذف نمایید.");
        }
    });
    $("#addEditPCPG").delegate("#PID", "change", (e) => {
        $("#addEditPCPG #PID[checked='checked']").removeAttr("checked");
        $(e.currentTarget).attr("checked", "checked");
    });
    $("#addEditPCPG").delegate("#Title", "keyup", () => {
        validateInput(PropTitle, "این فیلد نمی تواند خالی باشد.");
    });
    $("#addEditPCPG").delegate("#EnTitle", "keyup", () => {
        validateInput(PropEnTitle, "این فیلد نمی تواند خالی باشد.");
    });
    $("#addEditPCPG").delegate("#btnAddEdit", "click", () => {
        AddEditCatProp();
    });
    //#endregion
});

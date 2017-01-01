/// <reference path="jquery.min.js" />
$(function () {
    $('#list').datagrid({
        url: '/OfflineTrade/GetPagedList?_=' + GetGuid(),
        method: 'post',
        pagination: true,
        pageSize: 100,
        pageList: [100, 200, 300, 400],
        striped: true,
        rownumbers: true,
        fit: true,
        fitColumns: true,
        singleSelect: true,
        columns: [
            [
            {
               title: '交易单号', field: 'TradeCode', width: 100, sortable: true, formatter: function (val, row, index) {
                   return '<a title="点击查看详情" href=javascript:ToolFunction.Detail("' + row.TradeCode + '")  class=rowbtn' + '>' + row.TradeCode + '</a>';
               }
            },
             { title: '下单时间', field: 'TradeTime', width: 100, formatter: function (val, row, index) { return FormatDateTime(val); } },
             {
                 title: '下单账号信息', field: 'DealerName', width: 200, formatter: function (val, row, index) {
                     return '<p>所属经销商:' + (row.DealerName == null ? "" : row.DealerName) + '</p>' +
                         '<p><span style="margin-right:10px">下单账号：' + (row.UserName == null ? "" : row.UserName) + '</span>' +
                                   '<span> 显示名：' + (row.ShowName == null ? "" : row.ShowName) + '</span></p>';
                 }
             },
              { title: '商品数量', field: 'GoodsCount', width: 50 },
              { title: '交易单金额', field: 'ShowTotalMoney', width: 50 },
              { title: '确认金额', field: 'ShowConfirmedMoney', width: 50 },
              { title: '结算金额', field: 'ShowPaidMoney', width: 50 },
              {
                  title: '备注', field: 'OperateUser', width: 200, formatter: function (val, row, index) {
                      return '<p>账号:' + (row.OperateUser == null ? "" : row.OperateUser) + '(' + FormatDateTime(row.OperateTime) + ')</p>' +
                          '<p>备注文本：' + (row.Notes == null ? "" : row.Notes) + '</p>';
                  }
              },
              { title: '结算日期', field: 'OperateTime', width: 100, formatter: function (val, row, index) { return FormatDateTime(val); } }
            ]
        ],
        onLoadSuccess: function (data) {
            $(".span-TradeCount").text(data.TradeCount);
            $(".span-ShowTotalMoney").text(data.ShowTotalMoney);
            $(".span-ShowConfirmedMoney").text(data.ShowConfirmedMoney);
            $(".span-ShowPaidMoney").text(data.ShowPaidMoney);
        }
    })

});



var ToolFunction = {
    //搜索
    Search: function () {
        $('#list').datagrid('load', $("#form-search").serializeJson());
    },//重置所有条件
    Reset: function () {
        window.location.reload(true);
    }, //刷新
    Refresh: function () {
        $('#list').datagrid('reload');
    },
    Detail: function (code) {
        OpenDialog({
            href: "/Trade/Detail/" + code,
            title: "交易单详情",
            width: 1000,
            height: 500,
            resizable: true
        });
    },
    //结算
    TradeWaitingMoney: function (code) {
        OpenDialog({
            href: "/PaidTrade/InitPaidTrade?tradeCode=" + code,
            title: "结算",
            width: 600,
            height: 500,
            resizable: true,
            buttons: [
               {
                   text: '保存',
                   iconCls: 'icon-tick',
                   handler: ToolJS.Save
               }, {
                   text: '关闭',
                   iconCls: 'icon-cross',
                   handler: CloseDialog
               }
            ]
        });
    },
    //导出
    Export: function () {
        var conditionArray = $("#form-search").serializeJson();
        $AjaxDownloadFile({
            url: "/PaidTrade/OffLinePaidTradeExport",
            data: conditionArray
        });
    }
};


var ToolJS = {
    ///保存
    Save: function () {
        if ($('#form-detail').form('validate')) {
            var messager = '';

            var WaitingMoney = $("#ConfMoney").val();
            var ConfirmedMoney = $("#ConfirmedMoney").val();
            if (parseFloat(ConfirmedMoney) == parseFloat(WaitingMoney))
                messager = "结算金额与确定金额相符，是否确定";
            else
                messager = "结算金额与确认金额不符，是否继续结算(结算后无法修改) ";
            $.messager.confirm('提示', messager, function (r) {
                if (r) {
                    $Ajax({
                        url: "/PaidTrade/HandlerTradeWaitingMoney",
                        data: $("#form-detail").serializeArray(),
                        callBack: function (result) {
                            CloseDialog();
                            $.messager.alert("提示", result.Message, "info", function () {
                                ToolFunction.Refresh();
                            });
                        }
                    });
                }
            });

        }
    }
};

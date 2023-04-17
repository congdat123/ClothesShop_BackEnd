using CLOTHES_SHOP.Common.Enums;
using CLOTHES_SHOP.Helper.VNPay;
using CLOTHES_SHOP.IService.VNPay;
using CLOTHES_SHOP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using static CLOTHES_SHOP.Helper.VNPay.VNPayHelper;

namespace CLOTHES_SHOP.Service.VNPay
{
    public class VNPayService : IVNPayService
    {
        private readonly ClothesShopDbContext _context;
        public VNPayService(ClothesShopDbContext context)
        {
            _context = context;
        }

        public string CreateOrder(OrderInfo orderInfo)
        {
            //Get Config Info
            string vnp_Returnurl = "http://localhost:3000/payment/paymentsuccess"; //URL nhan ket qua tra ve 
            string vnp_Url = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html"; //URL thanh toan cua VNPAY 
            string vnp_TmnCode = "VW4CHF8Y"; //Ma website
            string vnp_HashSecret = "FEWXXWKLFFOTOINDLREISFEZVKOFHXBU"; //Chuoi bi mat
            //Get payment input
            OrderInfo order = new OrderInfo();
            //Save order to db
            order.OrderId = DateTime.Now.Ticks; // Giả lập mã giao dịch hệ thống merchant gửi sang VNPAY
            order.Amount = orderInfo.Amount; // Giả lập số tiền thanh toán hệ thống merchant gửi sang VNPAY 100,000 VND
            order.Status = "0"; //0: Trạng thái thanh toán "chờ thanh toán" hoặc "Pending"
            order.OrderDesc = orderInfo.OrderDesc;
            order.CreatedDate = DateTime.Now;
            string locale = orderInfo.Locale;
            //Build URL for VNPAY
            VNPayHelper vnpay = new VNPayHelper();

            vnpay.AddRequestData("vnp_Version", VNPayHelper.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", (order.Amount * 100).ToString()); //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY là: 10000000
            if (orderInfo.Vnp_BankCode != null && !string.IsNullOrEmpty(orderInfo.Vnp_BankCode))
            {
                vnpay.AddRequestData("vnp_BankCode", orderInfo.Vnp_BankCode);
            }
            vnpay.AddRequestData("vnp_CreateDate", order.CreatedDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetLocalIPAddress());
            if (!string.IsNullOrEmpty(locale))
            {
                vnpay.AddRequestData("vnp_Locale", locale);
            }
            else
            {
                vnpay.AddRequestData("vnp_Locale", "vn");
            }
            vnpay.AddRequestData("vnp_OrderInfo", orderInfo.ReferenceCode.ToString());
            vnpay.AddRequestData("vnp_OrderType", orderInfo.OrderCategory); //default value: other
            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", order.OrderId.ToString()); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày

            //Add Params of 2.1.0 Version
            vnpay.AddRequestData("vnp_ExpireDate", DateTime.Now.AddMinutes(15).ToString("yyyyMMddHHmmss"));

            //Save transaction to db
            var transaction = new Transaction()
            {
                /*Amount = orderInfo.Amount.ToString(),
                BillId = (int)orderInfo.ReferenceCode,
                PayDate = order.CreatedDate.ToString("yyyyMMdd"),
                Status = TransactionStatus.PENDING.ToString()*/
            };

            _context.Transactions.Add(transaction);
            _context.SaveChangesAsync();

            string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            return paymentUrl;
        }
    }
}
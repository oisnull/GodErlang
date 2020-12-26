using GodErlang.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace GodErlang.ConsoleTest
{
    public class InitConfig
    {
        public readonly static List<TestModel> TEST_URLS = new List<TestModel>()
        {
            new TestModel(){ SourceType = ProductSourceType.Microsoft, SourceUrl = "https://www.microsoft.com/en-sg/p/surface-pro-7/8n17j0m5zzqs?activetab=overview" },
            new TestModel(){ SourceType = ProductSourceType.Microsoft, SourceUrl = "https://www.microsoft.com/en-sg/p/xbox-wireless-controller/8XN59CRBSQGZ/3RSZ?cid=msft_web_collection&CustomerIntent=Consumer&headerId=Default-Store" },
            new TestModel(){ SourceType = ProductSourceType.Microsoft, SourceUrl = "https://www.microsoft.com/en-sg/p/surface-headphones-2/8wq0bsx7g523?activetab=pivot:overviewtab" },
            new TestModel(){ SourceType = ProductSourceType.Amazon, SourceUrl = "https://www.amazon.com/Garmin-Forerunner-Running-Watch-Black/dp/B0160BC1FO/?_encoding=UTF8&pd_rd_w=Zt38X&pf_rd_p=7c2c56ce-3b28-4a59-982c-2f5e4c0a2414&pf_rd_r=7YWC5T4JHWBSJJGZ01DC&pd_rd_r=db5a4d3b-8c77-49f1-8f8d-3c355ab26c1b&pd_rd_wg=68xOW&ref_=pd_gw_unk&th=1#" },
            new TestModel(){ SourceType = ProductSourceType.Amazon, SourceUrl = "https://www.amazon.com/gp/product/B08BB66H1R?pf_rd_r=4Q5MS2KDCZXSX6KSQA01&pf_rd_p=6fc81c8c-2a38-41c6-a68a-f78c79e7253f" },
            new TestModel(){ SourceType = ProductSourceType.Amazon, SourceUrl = "https://www.amazon.com/Thermos-Funtainer-Ounce-Food-Frozen/dp/B00MBMR7CO/?_encoding=UTF8&pd_rd_w=Wb6MH&pf_rd_p=9e5365f3-c3cd-44da-9774-5e9ed1516f1e&pf_rd_r=5GM3QSQMNA1JNCNP1RTX&pd_rd_r=3ea89bbe-c986-43d1-98ad-d36bd15eda21&pd_rd_wg=R5Tu0&ref_=pd_gw_unk" },
            new TestModel(){ SourceType = ProductSourceType.EBay, SourceUrl = "https://www.ebay.com/itm/Acer-Swift-1-Laptop-Intel-Pentium-Silver-N5000-1-1GHz-4GB-Ram-128GB-SDD-W10HS/254702731687?_trkparms=pageci%3Ab901d603-39f8-11eb-8fd2-16735b8a44cd%7Cparentrq%3A469f10831760aa65789e69e6fff537e0%7Ciid%3A1" },
            new TestModel(){ SourceType = ProductSourceType.EBay, SourceUrl = "https://www.ebay.com/itm/Apple-Macbook-13-Laptop-UPGRADED-8GB-RAM-1TB-HD-MAC-OS-2017-WARRANTY/333073810327?_trkparms=aid%3D1110009%26algo%3DSPLICE.COMPLISTINGS%26ao%3D1%26asc%3D20200220094952%26meid%3D21eab73b3346463cbd26dfa4326e3c58%26pid%3D100008%26rk%3D6%26rkt%3D12%26mehot%3Dpp%26sd%3D254702731687%26itm%3D333073810327%26pmt%3D1%26noa%3D0%26pg%3D2047675%26algv%3Ddefault%26brand%3DApple&_trksid=p2047675.c100008.m2219" },
            new TestModel(){ SourceType = ProductSourceType.EBay, SourceUrl = "https://www.ebay.com/itm/Logitech-G920-Driving-Force-Racing-Wheel-Dual-Motor-Force-Feedback-Xbox-and-PC/352378148393?epid=219519860&hash=item520b5fd629%3Ag%3AUbAAAOSwiHZbHvh7&_trkparms=%2526rpp_cid%253D5e5006cf89c0dc26ca2b058b" },
            new TestModel(){ SourceType = ProductSourceType.EBay, SourceUrl = "https://www.ebay.com/itm/Microsoft-Xbox-Wireless-Controller-Red-Xbox-One/373015756895?_trkparms=aid%3D1110012%26algo%3DSPLICE.SOIPOST%26ao%3D1%26asc%3D20200420083544%26meid%3Db0782bc014224d65826ef8917433f758%26pid%3D100008%26rk%3D3%26rkt%3D12%26mehot%3Dag%26sd%3D352378148393%26itm%3D373015756895%26pmt%3D1%26noa%3D0%26pg%3D2047675%26algv%3DPromotedSellersOtherItemsV2%26brand%3DMicrosoft&_trksid=p2047675.c100008.m2219" },
        };

    }

    public class TestModel
    {
        public ProductSourceType SourceType { get; set; }
        public string SourceUrl { get; set; }
    }
}

<%@ Page Title="" Language="C#" MasterPageFile="~/RayMaster.Master" AutoEventWireup="true"
    CodeBehind="Ray.aspx.cs" Inherits="WebRenderToy.Tester2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BodyPlaceHolder2" runat="server">
    <div id="raytracer">
        <table style="width: 800px; height: 600px" id="mainTable" border="0">
            <tr>
                <td style="width: 590px">
                    <div id="imgContainer" style="width: 590px; height: 300px; overflow: auto">
                        <img id="image" />
                    </div>
                    <div style="width: 590px; height: 300px">
                        <textarea id="code"><World Tracer="Whitted">
  <World.ViewPlane Width="300" Height="500" Samples="4" PixelSize="1" MaxDepth="5" Sampler="Regular"/>
  <World.AmbientLight>
    <Ambient Color="1,1,1" Radiance="1" Shadows="False"/>
  </World.AmbientLight>
  <World.Camera>
    <Pinhole Position="40,10,0" Target="0.1,0.5,0" Zoom="1.0" Distance="5000" ExposureTime="1" RollAngle="0"/>
  </World.Camera>
  <Lights>
    <PointLight Color="1,1,1" Position="4,4,0" Shadows="True" Radiance="1"/>
  </Lights>
  <Objects>
    <Mesh Shadows="False">
      <Mesh.Material>
        <Transparent AmbientCoeff="0.2" DiffuseCoeff="0.2" SpecularColor="1" SpecularCoeff="0.1" Exponent="2000" ReflectiveCoeff="0.1" IOR="1.6" TransCoeff="0.6" Color="1" Shadows="False"/>
      </Mesh.Material>
      <Mesh.Vertices>
		<Vertex Position='-0.2387692,1.31038,0.1300126'/>
		<Vertex Position='-0.2758292,1.258256,0.123646'/>
		<Vertex Position='-0.2674888,1.347437,0.1591275'/>
		<Vertex Position='-0.3128663,1.222713,0.1462357'/>
		<Vertex Position='-0.3018968,1.383255,0.1992584'/>
		<Vertex Position='-0.3291085,1.401578,0.2351516'/>
		<Vertex Position='-0.3393653,1.160645,0.2503386'/>
		<Vertex Position='-0.329337,1.387289,0.3029718'/>
		<Vertex Position='-0.3441395,1.184785,0.3161903'/>
		<Vertex Position='-0.327314,1.347217,0.3359971'/>
		<Vertex Position='-0.3199373,1.201023,0.3533615'/>
		<Vertex Position='-0.3072671,1.295644,0.3643486'/>
		<Vertex Position='-0.2877856,1.249347,0.3823087'/>
		<Vertex Position='-0.2103432,1.353167,0.153717'/>
		<Vertex Position='-0.2114563,1.381852,0.3517549'/>
		<Vertex Position='-0.2338628,1.344237,0.3794802'/>
		<Vertex Position='-0.2561403,1.295263,0.3911502'/>
		<Vertex Position='-0.5961054,-0.6768463,-0.9120215'/>
		<Vertex Position='-0.1000134,-0.9910442,-0.7028175'/>
		<Vertex Position='-0.1608638,-1.127278,-0.7434845'/>
		<Vertex Position='-0.008083896,-1.092125,-0.5243409'/>
		<Vertex Position='-0.07976516,-1.276867,-0.5252506'/>
		<Vertex Position='-0.02006051,-1.233439,-0.3013251'/>
		<Vertex Position='-0.0799078,-1.350904,-0.34107'/>
		<Vertex Position='-0.1016905,-1.206887,-0.04876722'/>
		<Vertex Position='-0.1519818,-1.365511,-0.1569244'/>
		<Vertex Position='-0.2601279,-1.18858,0.1098897'/>
		<Vertex Position='-0.3172958,-1.321268,0.05944163'/>
		<Vertex Position='-0.8485689,-0.9383249,0.2398284'/>
		<Vertex Position='-0.2841116,-1.207877,-0.8036506'/>
		<Vertex Position='-0.2223184,-1.370571,-0.6258835'/>
		<Vertex Position='-0.1980858,-1.452216,-0.3886956'/>
		<Vertex Position='-0.312712,-1.481223,-0.1662599'/>
		<Vertex Position='-0.4468655,-1.408121,0.03030982'/>
		<Vertex Position='-0.3988207,-0.9783604,-0.8833077'/>
		<Vertex Position='-0.4261847,-1.253856,-0.8326578'/>
		<Vertex Position='-0.3809143,-1.432361,-0.6713096'/>
		<Vertex Position='-0.3439267,-1.52964,-0.4359666'/>
		<Vertex Position='-0.4712127,-1.542748,-0.2127'/>
		<Vertex Position='-0.5840335,-1.449947,-0.01688966'/>
		<Vertex Position='-0.5965325,-1.231038,0.1543973'/>
		<Vertex Position='-0.6039706,-0.9730496,-0.9442929'/>
		<Vertex Position='-0.6584408,-1.233176,-0.8746206'/>
		<Vertex Position='-0.7094098,-1.465943,-0.715402'/>
		<Vertex Position='-0.6644813,-1.577168,-0.5104212'/>
		<Vertex Position='-0.7894059,-1.563823,-0.30882'/>
		<Vertex Position='-0.8162833,-1.431081,-0.05752848'/>
		<Vertex Position='-0.8117676,-1.23444,0.1341408'/>
		<Vertex Position='-0.2583476,-0.6951673,-0.7923673'/>
		<Vertex Position='-0.06248973,-0.7665911,-0.65404'/>
		<Vertex Position='0.0761124,-0.9156477,-0.2053677'/>
		<Vertex Position='-0.03108127,-0.8994758,0.04312489'/>
		<Vertex Position='-0.2509458,-0.9602191,0.1949888'/>
		<Vertex Position='-0.5314776,-0.9170871,0.2760487'/>
		<Vertex Position='-0.6982013,-0.8975001,-0.9599983'/>
		<Vertex Position='-0.8293666,-1.115095,-0.914649'/>
		<Vertex Position='-0.9619048,-1.343162,-0.7880316'/>
		<Vertex Position='-1.047212,-1.473044,-0.5605464'/>
		<Vertex Position='-1.060766,-1.464113,-0.2855664'/>
		<Vertex Position='-1.000425,-1.328387,-0.03361413'/>
		<Vertex Position='-0.9104906,-1.166207,0.1470253'/>
		<Vertex Position='-0.9586856,-0.6710832,-0.9367557'/>
		<Vertex Position='-1.244579,-0.817542,-0.8973407'/>
		<Vertex Position='-1.265987,-0.6801248,-0.8490487'/>
		<Vertex Position='-1.441736,-0.8928441,-0.7517044'/>
		<Vertex Position='-1.476803,-0.736002,-0.6685857'/>
		<Vertex Position='-1.563984,-0.9191614,-0.5287576'/>
		<Vertex Position='-1.568166,-0.7927271,-0.4991311'/>
		<Vertex Position='-1.532626,-1.003982,-0.2896925'/>
		<Vertex Position='-1.548949,-0.8242412,-0.3018251'/>
		<Vertex Position='-1.40789,-1.01804,-0.06605615'/>
		<Vertex Position='-1.422332,-0.872309,-0.05316658'/>
		<Vertex Position='-1.16522,-0.9337169,0.1334184'/>
		<Vertex Position='-1.125544,-0.4528789,-0.791549'/>
		<Vertex Position='-1.327921,-0.4237859,-0.605745'/>
		<Vertex Position='-1.453237,-0.505555,-0.4073495'/>
		<Vertex Position='-1.414896,-0.5304238,-0.163085'/>
		<Vertex Position='-1.289436,-0.6584542,0.04930571'/>
		<Vertex Position='-0.8874951,-0.8248029,-0.9565338'/>
		<Vertex Position='-1.165772,-0.9232322,-0.9113461'/>
		<Vertex Position='-1.373761,-1.05989,-0.765859'/>
		<Vertex Position='-1.501004,-1.093151,-0.5582768'/>
		<Vertex Position='-1.460103,-1.165444,-0.3270401'/>
		<Vertex Position='-1.330072,-1.126145,-0.07288784'/>
		<Vertex Position='-1.094028,-1.084367,0.1068209'/>
		<Vertex Position='-1.087594,-1.072288,-0.8950931'/>
		<Vertex Position='-1.24878,-1.199621,-0.7708104'/>
		<Vertex Position='-1.38718,-1.245335,-0.5725538'/>
		<Vertex Position='-1.333668,-1.303381,-0.3395301'/>
		<Vertex Position='-1.23866,-1.25869,-0.1234153'/>
		<Vertex Position='-0.8473896,-0.478493,-0.8710169'/>
		<Vertex Position='-1.062905,-0.336014,-0.7258101'/>
		<Vertex Position='-1.219074,-0.2962406,-0.563562'/>
		<Vertex Position='-1.351711,-0.3476974,-0.3492936'/>
		<Vertex Position='-1.309105,-0.4078128,-0.1033943'/>
		<Vertex Position='-1.216809,-0.527492,0.06741335'/>
		<Vertex Position='-1.05115,-0.7416804,0.2049141'/>
		<Vertex Position='-0.6820493,-0.4073454,-0.8362463'/>
		<Vertex Position='-0.8480347,-0.2239445,-0.6859356'/>
		<Vertex Position='-0.9382037,-0.08920602,-0.4560668'/>
		<Vertex Position='-1.089632,-0.1353774,-0.2476379'/>
		<Vertex Position='-1.017758,-0.1911239,0.0007070529'/>
		<Vertex Position='-1.034806,-0.4103816,0.1399093'/>
		<Vertex Position='-0.9396489,-0.6321303,0.2438743'/>
		<Vertex Position='-0.09560436,1.339214,0.147758'/>
		<Vertex Position='-0.1527351,1.295742,0.1099565'/>
		<Vertex Position='-0.06205053,1.369231,0.1955267'/>
		<Vertex Position='-0.0001521774,1.355462,0.3227203'/>
		<Vertex Position='-0.1034911,1.313798,0.4178816'/>
		<Vertex Position='-0.1508564,1.269496,0.4322599'/>
		<Vertex Position='-0.2143317,1.220212,0.4235984'/>
		<Vertex Position='-0.2191701,1.249857,0.0995416'/>
		<Vertex Position='-0.2824642,1.20504,0.1164464'/>
		<Vertex Position='-0.3129757,1.163166,0.1450396'/>
		<Vertex Position='-0.3266493,1.147169,0.329668'/>
		<Vertex Position='-0.2751151,1.178548,0.3885985'/>
		<Vertex Position='-0.07117825,1.283392,0.1015551'/>
		<Vertex Position='0.01952629,1.226838,0.08762971'/>
		<Vertex Position='-0.05337285,1.17809,0.04148648'/>
		<Vertex Position='-0.1215985,1.216039,0.06191305'/>
		<Vertex Position='-0.03216112,1.337285,0.1651154'/>
		<Vertex Position='0.07315566,1.265403,0.1618196'/>
		<Vertex Position='-0.01801212,1.364667,0.2298337'/>
		<Vertex Position='0.1036709,1.284341,0.252382'/>
		<Vertex Position='0.1314018,1.247816,0.327638'/>
		<Vertex Position='-0.05602181,1.329951,0.3930527'/>
		<Vertex Position='0.07522175,1.250714,0.395662'/>
		<Vertex Position='-0.08281235,1.270342,0.440224'/>
		<Vertex Position='0.01795294,1.197729,0.449576'/>
		<Vertex Position='-0.1400846,1.198131,0.4549248'/>
		<Vertex Position='-0.05573048,1.134205,0.4743454'/>
		<Vertex Position='-0.1958434,1.12508,0.4395501'/>
		<Vertex Position='-0.1364824,1.076299,0.4642072'/>
		<Vertex Position='-0.1320834,1.11888,0.03027101'/>
		<Vertex Position='-0.1778945,1.141988,0.05225012'/>
		<Vertex Position='-0.2024441,1.062628,0.05084088'/>
		<Vertex Position='-0.2321094,1.07965,0.07190571'/>
		<Vertex Position='-0.2603034,1.017915,0.1052719'/>
		<Vertex Position='-0.2952569,0.967508,0.1792173'/>
		<Vertex Position='-0.2882654,0.9677051,0.2798795'/>
		<Vertex Position='-0.2632514,0.9931304,0.3514136'/>
		<Vertex Position='-0.2864604,1.018558,0.3282173'/>
		<Vertex Position='-0.2086964,1.025945,0.4189417'/>
		<Vertex Position='-0.2467884,1.059545,0.3933816'/>
		<Vertex Position='0.1787231,1.025247,0.04349402'/>
		<Vertex Position='0.06748632,1.032837,-0.001200506'/>
		<Vertex Position='0.2561423,1.013628,0.1083551'/>
		<Vertex Position='0.3009503,0.9431608,0.2864319'/>
		<Vertex Position='0.1893684,0.931987,0.4481774'/>
		<Vertex Position='0.09325438,0.920481,0.4786886'/>
		<Vertex Position='-0.02687593,0.9171993,0.4788032'/>
		<Vertex Position='-0.05099148,1.030898,-0.008079474'/>
		<Vertex Position='-0.1620912,1.01595,0.02467676'/>
		<Vertex Position='-0.2291595,0.9354422,0.3685993'/>
		<Vertex Position='-0.1391732,0.9234519,0.441196'/>
		<Vertex Position='0.1786151,0.9029943,-0.005193119'/>
		<Vertex Position='0.2088507,0.5755958,-0.07667088'/>
		<Vertex Position='0.07966629,0.8444141,-0.05571698'/>
		<Vertex Position='0.1083653,0.5997704,-0.1099977'/>
		<Vertex Position='0.267799,0.9395661,0.07867891'/>
		<Vertex Position='0.2937291,0.9624594,0.1755266'/>
		<Vertex Position='0.3397881,0.4568369,0.176755'/>
		<Vertex Position='0.2527204,0.9123418,0.3839046'/>
		<Vertex Position='0.1960132,0.8515961,0.4438518'/>
		<Vertex Position='0.08617666,0.7897714,0.464854'/>
		<Vertex Position='0.1169563,0.4630117,0.3908241'/>
		<Vertex Position='-0.01881247,0.7237549,0.4452749'/>
		<Vertex Position='0.009515025,0.4786758,0.3928715'/>
		<Vertex Position='-0.01290514,0.6217365,-0.1077403'/>
		<Vertex Position='-0.02054241,0.7766144,-0.07025333'/>
		<Vertex Position='-0.1142033,0.6301372,-0.07232011'/>
		<Vertex Position='-0.1202734,0.7162054,-0.04479408'/>
		<Vertex Position='-0.194696,0.6638013,0.001950982'/>
		<Vertex Position='-0.2486527,0.5979938,0.09451063'/>
		<Vertex Position='-0.2377351,0.6112776,0.2189282'/>
		<Vertex Position='-0.1863437,0.5413926,0.2949494'/>
		<Vertex Position='-0.1895051,0.6312094,0.3081881'/>
		<Vertex Position='-0.104273,0.5097569,0.3570578'/>
		<Vertex Position='-0.1109892,0.6655312,0.3915983'/>
		<Vertex Position='0.1876342,0.5033062,-0.1048327'/>
		<Vertex Position='0.09809358,0.1757606,-0.2266011'/>
		<Vertex Position='0.00099806,0.2776941,-0.2383372'/>
		<Vertex Position='0.06202605,0.5143367,-0.1443024'/>
		<Vertex Position='0.2601772,0.5132496,-0.04198261'/>
		<Vertex Position='0.2267034,-0.07934967,0.02556359'/>
		<Vertex Position='0.1824864,0.4180674,0.3532599'/>
		<Vertex Position='0.09338059,0.3878325,0.3746745'/>
		<Vertex Position='-0.005211607,0.04917999,0.2990491'/>
		<Vertex Position='-0.03849107,0.391209,0.3670169'/>
		<Vertex Position='-0.1084806,0.1435513,0.3186572'/>
		<Vertex Position='-0.09324172,0.3598705,-0.2074026'/>
		<Vertex Position='-0.06719701,0.506645,-0.1397429'/>
		<Vertex Position='-0.1804474,0.4174666,-0.1415871'/>
		<Vertex Position='-0.1640469,0.5008696,-0.09943714'/>
		<Vertex Position='-0.2499161,0.4614254,-0.04084434'/>
		<Vertex Position='-0.2876141,0.4473778,0.05105445'/>
		<Vertex Position='-0.2867586,0.4162955,0.1463596'/>
		<Vertex Position='-0.2545006,0.32669,0.2353155'/>
		<Vertex Position='-0.231372,0.4183427,0.2428076'/>
		<Vertex Position='-0.192079,0.2387473,0.2955037'/>
		<Vertex Position='-0.1562875,0.3974921,0.3135334'/>
		<Vertex Position='-0.01872075,0.06242727,-0.3423576'/>
		<Vertex Position='-0.05158962,-0.2563934,-0.4980058'/>
		<Vertex Position='-0.200885,-0.1312641,-0.5453442'/>
		<Vertex Position='-0.1834077,0.09825169,-0.3943097'/>
		<Vertex Position='0.1211537,0.01442022,-0.2403778'/>
		<Vertex Position='0.05655945,-0.3606246,-0.4069227'/>
		<Vertex Position='0.1221258,-0.6050598,-0.124373'/>
		<Vertex Position='0.0252583,-0.1032294,0.2476899'/>
		<Vertex Position='-0.06640267,-0.501031,0.1972316'/>
		<Vertex Position='-0.1453849,-0.09303562,0.3026991'/>
		<Vertex Position='-0.2080531,-0.4402766,0.278544'/>
		<Vertex Position='-0.3201689,-0.06947275,0.3022'/>
		<Vertex Position='-0.376423,-0.3372842,0.3196175'/>
		<Vertex Position='-0.3710384,-0.01834371,-0.5181696'/>
		<Vertex Position='-0.3624876,0.1179233,-0.3885157'/>
		<Vertex Position='-0.5369908,0.05987335,-0.4200706'/>
		<Vertex Position='-0.4972249,0.134107,-0.3286189'/>
		<Vertex Position='-0.6294317,0.1155611,-0.2639131'/>
		<Vertex Position='-0.741735,0.0805378,-0.1252275'/>
		<Vertex Position='-0.6901485,0.04213846,0.04298647'/>
		<Vertex Position='-0.6583061,-0.08271667,0.1790937'/>
		<Vertex Position='-0.5930779,0.01940614,0.1508385'/>
		<Vertex Position='-0.5336281,-0.2084575,0.2850729'/>
		<Vertex Position='-0.486342,-0.03308446,0.2417778'/>
		<Vertex Position='-0.1642142,-0.3009066,-0.61347'/>
		<Vertex Position='-0.1912165,-0.4973635,-0.7024016'/>
		<Vertex Position='-0.3804429,-0.3517581,-0.7461891'/>
		<Vertex Position='-0.3691161,-0.1992443,-0.6606717'/>
		<Vertex Position='0.00764145,-0.4159198,-0.4894914'/>
		<Vertex Position='-0.007031327,-0.6391523,-0.5705633'/>
		<Vertex Position='0.1108236,-0.4698478,-0.3169906'/>
		<Vertex Position='0.07681783,-0.7589169,-0.4146954'/>
		<Vertex Position='0.02393247,-0.5670302,0.0963217'/>
		<Vertex Position='-0.1396981,-0.5845442,0.2243288'/>
		<Vertex Position='-0.1290935,-0.8245213,0.1719799'/>
		<Vertex Position='-0.3562968,-0.5164556,0.314674'/>
		<Vertex Position='-0.3207144,-0.7555247,0.2754698'/>
		<Vertex Position='-0.5751667,-0.4298021,0.3247894'/>
		<Vertex Position='-0.5608819,-0.6300128,0.3256889'/>
		<Vertex Position='-0.6119994,-0.2039437,-0.7058986'/>
		<Vertex Position='-0.5690995,-0.1206287,-0.6367858'/>
		<Vertex Position='-0.7745481,-0.08299685,-0.5533066'/>
		<Vertex Position='-0.9059965,-0.2757943,0.1565711'/>
		<Vertex Position='-0.7733492,-0.4852475,0.2900036'/>
		<Vertex Position='-0.7448096,-0.3254807,0.2656397'/>
		<Vertex Position='-0.1292811,0.2454123,-0.2781364'/>
		<Vertex Position='-0.01272218,0.1787235,-0.2856443'/>
		<Vertex Position='-0.2428141,0.2939574,-0.2234906'/>
		<Vertex Position='-0.3411247,0.3190121,-0.1272478'/>
		<Vertex Position='-0.3992092,0.3107304,-0.003677674'/>
		<Vertex Position='-0.3902996,0.2589536,0.1226432'/>
		<Vertex Position='-0.3311243,0.1856072,0.2262175'/>
		<Vertex Position='-0.2409134,0.1083565,0.2905483'/>
		<Vertex Position='-0.1295101,0.03536833,0.3092523'/>
		<Vertex Position='0.09116388,0.1090545,-0.2490301'/>
		<Vertex Position='0.1689329,0.0953466,-0.1748753'/>
		<Vertex Position='0.08621996,-0.006153152,0.2465113'/>
		<Vertex Position='-0.01383968,-0.01976756,0.285771'/>
		<Vertex Position='-0.1422447,1.383152,0.301441'/>
		<Vertex Position='-0.1360561,1.393858,0.276614'/>
		<Vertex Position='-0.1300272,1.393428,0.2520686'/>
		<Vertex Position='-0.1242392,1.38877,0.2306699'/>
		<Vertex Position='-0.1951844,1.392851,0.2037877'/>
		<Vertex Position='-0.2414877,1.39454,0.230161'/>
		<Vertex Position='-0.2579102,1.403741,0.2478713'/>
		<Vertex Position='-0.2620654,1.395744,0.2845279'/>
		<Vertex Position='-0.2013987,1.389485,0.3234921'/>
		<Vertex Position='-0.1488091,1.378121,0.3200274'/>
		<Vertex Position='-0.1315819,1.425068,0.2807315'/>
		<Vertex Position='-0.1159564,1.424568,0.22164'/>
		<Vertex Position='-0.2062742,1.412491,0.1874168'/>
		<Vertex Position='-0.2644544,1.400769,0.2212229'/>
		<Vertex Position='-0.2872685,1.406883,0.2444293'/>
		<Vertex Position='-0.2899361,1.393584,0.2908483'/>
		<Vertex Position='-0.2117856,1.399492,0.3403902'/>
		<Vertex Position='-0.1429001,1.399398,0.3353182'/>
		<Vertex Position='-0.2857648,1.518747,0.3082338'/>
		<Vertex Position='-0.2772762,1.530338,0.2625549'/>
		<Vertex Position='-0.3334605,1.488034,0.2330884'/>
		<Vertex Position='-0.3660409,1.453056,0.2571326'/>
		<Vertex Position='-0.3828999,1.445471,0.2753298'/>
		<Vertex Position='-0.3775182,1.431347,0.3100695'/>
		<Vertex Position='-0.3263152,1.463132,0.3506158'/>
		<Vertex Position='-0.2807144,1.492141,0.3479623'/>
		<Vertex Position='-0.3597184,1.566742,0.3249333'/>
		<Vertex Position='-0.3560427,1.584352,0.2784455'/>
		<Vertex Position='-0.3980452,1.526292,0.2443038'/>
		<Vertex Position='-0.4181656,1.479291,0.2664081'/>
		<Vertex Position='-0.4319479,1.46489,0.2846242'/>
		<Vertex Position='-0.4213054,1.450197,0.3196409'/>
		<Vertex Position='-0.3805278,1.495432,0.3644531'/>
		<Vertex Position='-0.3451782,1.539286,0.3641737'/>
		<Vertex Position='-0.5252532,1.579776,0.361834'/>
		<Vertex Position='-0.5427782,1.600036,0.3211358'/>
		<Vertex Position='-0.5227646,1.541824,0.2739055'/>
		<Vertex Position='-0.4933105,1.497233,0.2843138'/>
		<Vertex Position='-0.4871033,1.47541,0.2977991'/>
		<Vertex Position='-0.4654757,1.470253,0.3292893'/>
		<Vertex Position='-0.4746048,1.518937,0.3841418'/>
		<Vertex Position='-0.4907531,1.570393,0.3952265'/>
		<Vertex Position='-0.6190131,1.569656,0.3800323'/>
		<Vertex Position='-0.635502,1.585899,0.3462182'/>
		<Vertex Position='-0.6139504,1.539806,0.3053658'/>
		<Vertex Position='-0.5784211,1.486814,0.3241443'/>
		<Vertex Position='-0.5598035,1.483728,0.3505057'/>
		<Vertex Position='-0.57142,1.522628,0.3975746'/>
		<Vertex Position='-0.5892615,1.56409,0.4079022'/>
		<Vertex Position='-0.7207897,1.556779,0.4011635'/>
		<Vertex Position='-0.7290468,1.572664,0.370292'/>
		<Vertex Position='-0.727075,1.525484,0.3386745'/>
		<Vertex Position='-0.7156067,1.472703,0.35959'/>
		<Vertex Position='-0.7013828,1.466133,0.3830785'/>
		<Vertex Position='-0.6976434,1.504754,0.4208247'/>
		<Vertex Position='-0.6983812,1.544691,0.4263767'/>
		<Vertex Position='-0.9070526,1.496081,0.417258'/>
		<Vertex Position='-0.886304,1.474514,0.3901551'/>
		<Vertex Position='-0.8554405,1.448989,0.3996098'/>
		<Vertex Position='-0.8436098,1.449492,0.4158023'/>
		<Vertex Position='-0.875124,1.489145,0.4545401'/>
		<Vertex Position='-0.9884731,1.436911,0.4354714'/>
		<Vertex Position='-1.009751,1.402303,0.4153955'/>
		<Vertex Position='-1.026166,1.365391,0.4377826'/>
		<Vertex Position='-1.019388,1.357156,0.4576102'/>
		<Vertex Position='-0.9801045,1.410878,0.4839257'/>
		<Vertex Position='-1.064602,1.37547,0.4676548'/>
	</Mesh.Vertices>
	<Mesh.Faces>
		<Face Indices='0,1,2'/>
		<Face Indices='3,4,2'/>
		<Face Indices='3,2,1'/>
		<Face Indices='8,9,6'/>
		<Face Indices='9,7,6'/>
		<Face Indices='10,11,8'/>
		<Face Indices='11,9,8'/>
		<Face Indices='11,10,12'/>
		<Face Indices='0,2,13'/>
		<Face Indices='9,15,14'/>
		<Face Indices='9,14,7'/>
		<Face Indices='11,16,15'/>
		<Face Indices='11,15,9'/>
		<Face Indices='16,11,12'/>
		<Face Indices='20,21,19'/>
		<Face Indices='20,19,18'/>
		<Face Indices='22,23,21'/>
		<Face Indices='22,21,20'/>
		<Face Indices='24,25,22'/>
		<Face Indices='25,23,22'/>
		<Face Indices='26,27,24'/>
		<Face Indices='27,25,24'/>
		<Face Indices='21,30,19'/>
		<Face Indices='30,29,19'/>
		<Face Indices='23,31,21'/>
		<Face Indices='31,30,21'/>
		<Face Indices='25,32,31'/>
		<Face Indices='25,31,23'/>
		<Face Indices='27,33,32'/>
		<Face Indices='27,32,25'/>
		<Face Indices='30,36,29'/>
		<Face Indices='36,35,29'/>
		<Face Indices='31,37,30'/>
		<Face Indices='37,36,30'/>
		<Face Indices='32,38,37'/>
		<Face Indices='32,37,31'/>
		<Face Indices='33,39,38'/>
		<Face Indices='33,38,32'/>
		<Face Indices='17,34,41'/>
		<Face Indices='35,42,34'/>
		<Face Indices='42,41,34'/>
		<Face Indices='36,43,35'/>
		<Face Indices='43,42,35'/>
		<Face Indices='37,44,36'/>
		<Face Indices='44,43,36'/>
		<Face Indices='38,45,44'/>
		<Face Indices='38,44,37'/>
		<Face Indices='39,46,45'/>
		<Face Indices='39,45,38'/>
		<Face Indices='40,47,46'/>
		<Face Indices='40,46,39'/>
		<Face Indices='47,40,28'/>
		<Face Indices='50,22,20'/>
		<Face Indices='51,24,50'/>
		<Face Indices='24,22,50'/>
		<Face Indices='52,26,51'/>
		<Face Indices='26,24,51'/>
		<Face Indices='17,41,54'/>
		<Face Indices='42,55,41'/>
		<Face Indices='55,54,41'/>
		<Face Indices='43,56,42'/>
		<Face Indices='56,55,42'/>
		<Face Indices='44,57,43'/>
		<Face Indices='57,56,43'/>
		<Face Indices='45,58,57'/>
		<Face Indices='45,57,44'/>
		<Face Indices='46,59,58'/>
		<Face Indices='46,58,45'/>
		<Face Indices='47,60,59'/>
		<Face Indices='47,59,46'/>
		<Face Indices='60,47,28'/>
		<Face Indices='62,63,61'/>
		<Face Indices='64,65,63'/>
		<Face Indices='64,63,62'/>
		<Face Indices='66,67,65'/>
		<Face Indices='66,65,64'/>
		<Face Indices='68,69,66'/>
		<Face Indices='69,67,66'/>
		<Face Indices='70,71,68'/>
		<Face Indices='71,69,68'/>
		<Face Indices='72,71,70'/>
		<Face Indices='63,73,61'/>
		<Face Indices='65,74,63'/>
		<Face Indices='74,73,63'/>
		<Face Indices='67,75,65'/>
		<Face Indices='75,74,65'/>
		<Face Indices='69,76,75'/>
		<Face Indices='69,75,67'/>
		<Face Indices='71,77,76'/>
		<Face Indices='71,76,69'/>
		<Face Indices='72,77,71'/>
		<Face Indices='80,64,62'/>
		<Face Indices='80,62,79'/>
		<Face Indices='81,66,64'/>
		<Face Indices='81,64,80'/>
		<Face Indices='82,68,81'/>
		<Face Indices='68,66,81'/>
		<Face Indices='83,70,82'/>
		<Face Indices='70,68,82'/>
		<Face Indices='85,79,78'/>
		<Face Indices='86,80,79'/>
		<Face Indices='86,79,85'/>
		<Face Indices='87,81,80'/>
		<Face Indices='87,80,86'/>
		<Face Indices='88,82,87'/>
		<Face Indices='82,81,87'/>
		<Face Indices='89,83,88'/>
		<Face Indices='83,82,88'/>
		<Face Indices='84,83,89'/>
		<Face Indices='56,86,85'/>
		<Face Indices='56,85,55'/>
		<Face Indices='57,87,86'/>
		<Face Indices='57,86,56'/>
		<Face Indices='58,88,57'/>
		<Face Indices='88,87,57'/>
		<Face Indices='59,89,58'/>
		<Face Indices='89,88,58'/>
		<Face Indices='74,92,73'/>
		<Face Indices='92,91,73'/>
		<Face Indices='75,93,74'/>
		<Face Indices='93,92,74'/>
		<Face Indices='76,94,93'/>
		<Face Indices='76,93,75'/>
		<Face Indices='77,95,94'/>
		<Face Indices='77,94,76'/>
		<Face Indices='17,90,97'/>
		<Face Indices='91,98,90'/>
		<Face Indices='98,97,90'/>
		<Face Indices='92,99,91'/>
		<Face Indices='99,98,91'/>
		<Face Indices='93,100,92'/>
		<Face Indices='100,99,92'/>
		<Face Indices='94,101,100'/>
		<Face Indices='94,100,93'/>
		<Face Indices='95,102,101'/>
		<Face Indices='95,101,94'/>
		<Face Indices='96,103,102'/>
		<Face Indices='96,102,95'/>
		<Face Indices='103,96,28'/>
		<Face Indices='13,104,105'/>
		<Face Indices='13,105,0'/>
		<Face Indices='15,108,14'/>
		<Face Indices='16,109,15'/>
		<Face Indices='109,108,15'/>
		<Face Indices='12,110,16'/>
		<Face Indices='110,109,16'/>
		<Face Indices='111,1,0'/>
		<Face Indices='111,0,105'/>
		<Face Indices='112,3,1'/>
		<Face Indices='112,1,111'/>
		<Face Indices='113,3,112'/>
		<Face Indices='115,10,114'/>
		<Face Indices='10,8,114'/>
		<Face Indices='110,12,115'/>
		<Face Indices='12,10,115'/>
		<Face Indices='116,117,118'/>
		<Face Indices='116,118,119'/>
		<Face Indices='120,121,117'/>
		<Face Indices='120,117,116'/>
		<Face Indices='122,123,121'/>
		<Face Indices='122,121,120'/>
		<Face Indices='107,124,123'/>
		<Face Indices='107,123,122'/>
		<Face Indices='125,126,107'/>
		<Face Indices='126,124,107'/>
		<Face Indices='127,128,125'/>
		<Face Indices='128,126,125'/>
		<Face Indices='129,130,127'/>
		<Face Indices='130,128,127'/>
		<Face Indices='131,132,129'/>
		<Face Indices='132,130,129'/>
		<Face Indices='133,134,119'/>
		<Face Indices='133,119,118'/>
		<Face Indices='135,136,134'/>
		<Face Indices='135,134,133'/>
		<Face Indices='142,143,140'/>
		<Face Indices='143,141,140'/>
		<Face Indices='132,131,142'/>
		<Face Indices='131,143,142'/>
		<Face Indices='117,144,145'/>
		<Face Indices='117,145,118'/>
		<Face Indices='121,146,144'/>
		<Face Indices='121,144,117'/>
		<Face Indices='123,146,121'/>
		<Face Indices='128,148,126'/>
		<Face Indices='130,149,128'/>
		<Face Indices='149,148,128'/>
		<Face Indices='132,150,130'/>
		<Face Indices='150,149,130'/>
		<Face Indices='151,133,118'/>
		<Face Indices='151,118,145'/>
		<Face Indices='152,135,133'/>
		<Face Indices='152,133,151'/>
		<Face Indices='154,142,153'/>
		<Face Indices='142,140,153'/>
		<Face Indices='150,132,154'/>
		<Face Indices='132,142,154'/>
		<Face Indices='155,156,157'/>
		<Face Indices='156,158,157'/>
		<Face Indices='147,161,160'/>
		<Face Indices='162,161,147'/>
		<Face Indices='166,167,165'/>
		<Face Indices='166,165,164'/>
		<Face Indices='168,169,158'/>
		<Face Indices='169,157,158'/>
		<Face Indices='170,171,168'/>
		<Face Indices='171,169,168'/>
		<Face Indices='172,171,170'/>
		<Face Indices='175,176,174'/>
		<Face Indices='177,178,176'/>
		<Face Indices='177,176,175'/>
		<Face Indices='167,166,178'/>
		<Face Indices='167,178,177'/>
		<Face Indices='179,180,181'/>
		<Face Indices='179,181,182'/>
		<Face Indices='183,180,179'/>
		<Face Indices='186,187,185'/>
		<Face Indices='188,189,186'/>
		<Face Indices='189,187,186'/>
		<Face Indices='190,191,182'/>
		<Face Indices='190,182,181'/>
		<Face Indices='192,193,191'/>
		<Face Indices='192,191,190'/>
		<Face Indices='199,200,197'/>
		<Face Indices='200,198,197'/>
		<Face Indices='189,188,199'/>
		<Face Indices='188,200,199'/>
		<Face Indices='201,202,203'/>
		<Face Indices='201,203,204'/>
		<Face Indices='205,206,202'/>
		<Face Indices='205,202,201'/>
		<Face Indices='210,211,208'/>
		<Face Indices='211,209,208'/>
		<Face Indices='212,213,210'/>
		<Face Indices='213,211,210'/>
		<Face Indices='214,215,204'/>
		<Face Indices='214,204,203'/>
		<Face Indices='216,217,215'/>
		<Face Indices='216,215,214'/>
		<Face Indices='223,224,221'/>
		<Face Indices='224,222,221'/>
		<Face Indices='213,212,223'/>
		<Face Indices='212,224,223'/>
		<Face Indices='225,226,227'/>
		<Face Indices='225,227,228'/>
		<Face Indices='229,230,226'/>
		<Face Indices='229,226,225'/>
		<Face Indices='231,232,230'/>
		<Face Indices='231,230,229'/>
		<Face Indices='207,50,232'/>
		<Face Indices='207,232,231'/>
		<Face Indices='234,235,233'/>
		<Face Indices='236,237,234'/>
		<Face Indices='237,235,234'/>
		<Face Indices='238,239,236'/>
		<Face Indices='239,237,236'/>
		<Face Indices='240,241,228'/>
		<Face Indices='240,228,227'/>
		<Face Indices='239,238,244'/>
		<Face Indices='238,245,244'/>
		<Face Indices='241,214,228'/>
		<Face Indices='214,203,228'/>
		<Face Indices='242,216,241'/>
		<Face Indices='216,214,241'/>
		<Face Indices='245,223,221'/>
		<Face Indices='245,221,243'/>
		<Face Indices='238,213,223'/>
		<Face Indices='238,223,245'/>
		<Face Indices='202,225,203'/>
		<Face Indices='225,228,203'/>
		<Face Indices='206,229,202'/>
		<Face Indices='229,225,202'/>
		<Face Indices='231,229,206'/>
		<Face Indices='209,234,233'/>
		<Face Indices='211,236,234'/>
		<Face Indices='211,234,209'/>
		<Face Indices='213,238,236'/>
		<Face Indices='213,236,211'/>
		<Face Indices='215,246,204'/>
		<Face Indices='246,247,204'/>
		<Face Indices='217,248,215'/>
		<Face Indices='248,246,215'/>
		<Face Indices='218,249,217'/>
		<Face Indices='249,248,217'/>
		<Face Indices='219,250,218'/>
		<Face Indices='250,249,218'/>
		<Face Indices='220,251,250'/>
		<Face Indices='220,250,219'/>
		<Face Indices='222,252,251'/>
		<Face Indices='222,251,220'/>
		<Face Indices='224,253,252'/>
		<Face Indices='224,252,222'/>
		<Face Indices='212,254,253'/>
		<Face Indices='212,253,224'/>
		<Face Indices='255,201,247'/>
		<Face Indices='201,204,247'/>
		<Face Indices='256,205,255'/>
		<Face Indices='205,201,255'/>
		<Face Indices='258,210,208'/>
		<Face Indices='258,208,257'/>
		<Face Indices='254,212,210'/>
		<Face Indices='254,210,258'/>
		<Face Indices='246,190,181'/>
		<Face Indices='246,181,247'/>
		<Face Indices='248,192,190'/>
		<Face Indices='248,190,246'/>
		<Face Indices='249,192,248'/>
		<Face Indices='252,197,251'/>
		<Face Indices='253,199,252'/>
		<Face Indices='199,197,252'/>
		<Face Indices='254,189,253'/>
		<Face Indices='189,199,253'/>
		<Face Indices='180,255,247'/>
		<Face Indices='180,247,181'/>
		<Face Indices='189,254,187'/>
		<Face Indices='254,258,187'/>
		<Face Indices='191,168,182'/>
		<Face Indices='168,158,182'/>
		<Face Indices='193,170,191'/>
		<Face Indices='170,168,191'/>
		<Face Indices='195,173,194'/>
		<Face Indices='196,173,195'/>
		<Face Indices='200,177,175'/>
		<Face Indices='200,175,198'/>
		<Face Indices='188,167,177'/>
		<Face Indices='188,177,200'/>
		<Face Indices='156,179,158'/>
		<Face Indices='179,182,158'/>
		<Face Indices='183,179,156'/>
		<Face Indices='165,186,185'/>
		<Face Indices='167,188,186'/>
		<Face Indices='167,186,165'/>
		<Face Indices='169,151,157'/>
		<Face Indices='151,145,157'/>
		<Face Indices='171,152,169'/>
		<Face Indices='152,151,169'/>
		<Face Indices='173,138,172'/>
		<Face Indices='174,138,173'/>
		<Face Indices='178,154,153'/>
		<Face Indices='178,153,176'/>
		<Face Indices='166,150,154'/>
		<Face Indices='166,154,178'/>
		<Face Indices='144,155,145'/>
		<Face Indices='155,157,145'/>
		<Face Indices='146,159,144'/>
		<Face Indices='159,155,144'/>
		<Face Indices='160,159,146'/>
		<Face Indices='148,163,162'/>
		<Face Indices='149,164,163'/>
		<Face Indices='149,163,148'/>
		<Face Indices='150,166,164'/>
		<Face Indices='150,164,149'/>
		<Face Indices='134,111,119'/>
		<Face Indices='111,105,119'/>
		<Face Indices='136,112,134'/>
		<Face Indices='112,111,134'/>
		<Face Indices='137,113,136'/>
		<Face Indices='113,112,136'/>
		<Face Indices='143,115,114'/>
		<Face Indices='143,114,141'/>
		<Face Indices='131,110,115'/>
		<Face Indices='131,115,143'/>
		<Face Indices='104,116,105'/>
		<Face Indices='116,119,105'/>
		<Face Indices='106,120,104'/>
		<Face Indices='120,116,104'/>
		<Face Indices='122,120,106'/>
		<Face Indices='108,127,125'/>
		<Face Indices='109,129,127'/>
		<Face Indices='109,127,108'/>
		<Face Indices='110,131,129'/>
		<Face Indices='110,129,109'/>
		<Face Indices='28,239,244'/>
		<Face Indices='28,244,103'/>
		<Face Indices='240,97,98'/>
		<Face Indices='227,17,97'/>
		<Face Indices='227,97,240'/>
		<Face Indices='235,52,51'/>
		<Face Indices='237,53,52'/>
		<Face Indices='237,52,235'/>
		<Face Indices='239,28,53'/>
		<Face Indices='239,53,237'/>
		<Face Indices='49,230,232'/>
		<Face Indices='48,226,230'/>
		<Face Indices='48,230,49'/>
		<Face Indices='17,227,226'/>
		<Face Indices='17,226,48'/>
		<Face Indices='7,5,6'/>
		<Face Indices='6,5,4'/>
		<Face Indices='113,6,3'/>
		<Face Indices='6,113,137'/>
		<Face Indices='138,6,137'/>
		<Face Indices='139,6,138'/>
		<Face Indices='6,4,3'/>
		<Face Indices='17,48,34'/>
		<Face Indices='19,29,34'/>
		<Face Indices='29,35,34'/>
		<Face Indices='18,19,34'/>
		<Face Indices='49,18,34'/>
		<Face Indices='49,34,48'/>
		<Face Indices='40,33,27'/>
		<Face Indices='40,39,33'/>
		<Face Indices='40,53,28'/>
		<Face Indices='40,26,52'/>
		<Face Indices='40,27,26'/>
		<Face Indices='53,40,52'/>
		<Face Indices='17,78,61'/>
		<Face Indices='79,62,61'/>
		<Face Indices='79,61,78'/>
		<Face Indices='72,70,83'/>
		<Face Indices='72,84,28'/>
		<Face Indices='84,72,83'/>
		<Face Indices='17,54,78'/>
		<Face Indices='55,85,78'/>
		<Face Indices='55,78,54'/>
		<Face Indices='84,89,59'/>
		<Face Indices='84,60,28'/>
		<Face Indices='60,84,59'/>
		<Face Indices='17,61,90'/>
		<Face Indices='73,90,61'/>
		<Face Indices='73,91,90'/>
		<Face Indices='72,96,77'/>
		<Face Indices='96,72,28'/>
		<Face Indices='96,95,77'/>
		<Face Indices='114,8,6'/>
		<Face Indices='141,114,6'/>
		<Face Indices='141,6,139'/>
		<Face Indices='137,136,135'/>
		<Face Indices='140,141,139'/>
		<Face Indices='172,170,193'/>
		<Face Indices='173,172,194'/>
		<Face Indices='194,172,193'/>
		<Face Indices='196,174,173'/>
		<Face Indices='198,175,174'/>
		<Face Indices='198,174,196'/>
		<Face Indices='194,193,192'/>
		<Face Indices='249,194,192'/>
		<Face Indices='250,195,194'/>
		<Face Indices='250,194,249'/>
		<Face Indices='196,195,250'/>
		<Face Indices='197,198,196'/>
		<Face Indices='197,196,251'/>
		<Face Indices='251,196,250'/>
		<Face Indices='218,217,216'/>
		<Face Indices='218,216,242'/>
		<Face Indices='221,222,220'/>
		<Face Indices='243,221,220'/>
		<Face Indices='242,241,240'/>
		<Face Indices='240,98,242'/>
		<Face Indices='242,98,99'/>
		<Face Indices='244,245,243'/>
		<Face Indices='103,244,243'/>
		<Face Indices='103,243,102'/>
		<Face Indices='184,207,231'/>
		<Face Indices='233,207,184'/>
		<Face Indices='183,256,180'/>
		<Face Indices='256,255,180'/>
		<Face Indices='187,257,185'/>
		<Face Indices='187,258,257'/>
		<Face Indices='159,183,155'/>
		<Face Indices='183,156,155'/>
		<Face Indices='164,185,163'/>
		<Face Indices='164,165,185'/>
		<Face Indices='123,160,146'/>
		<Face Indices='124,160,123'/>
		<Face Indices='124,147,160'/>
		<Face Indices='126,162,124'/>
		<Face Indices='148,162,126'/>
		<Face Indices='162,147,124'/>
		<Face Indices='108,125,14'/>
		<Face Indices='233,51,207'/>
		<Face Indices='235,51,233'/>
		<Face Indices='51,50,207'/>
		<Face Indices='232,20,18'/>
		<Face Indices='50,20,232'/>
		<Face Indices='232,18,49'/>
		<Face Indices='137,135,152'/>
		<Face Indices='137,152,171'/>
		<Face Indices='138,137,172'/>
		<Face Indices='172,137,171'/>
		<Face Indices='174,139,138'/>
		<Face Indices='153,140,139'/>
		<Face Indices='176,153,139'/>
		<Face Indices='176,139,174'/>
		<Face Indices='99,218,242'/>
		<Face Indices='100,219,99'/>
		<Face Indices='219,218,99'/>
		<Face Indices='101,219,100'/>
		<Face Indices='102,243,101'/>
		<Face Indices='243,220,101'/>
		<Face Indices='101,220,219'/>
		<Face Indices='268,259,276'/>
		<Face Indices='269,276,259'/>
		<Face Indices='259,260,269'/>
		<Face Indices='260,261,269'/>
		<Face Indices='270,269,261'/>
		<Face Indices='261,262,270'/>
		<Face Indices='262,263,270'/>
		<Face Indices='271,270,263'/>
		<Face Indices='263,264,271'/>
		<Face Indices='272,271,264'/>
		<Face Indices='264,265,272'/>
		<Face Indices='273,272,265'/>
		<Face Indices='265,266,273'/>
		<Face Indices='274,273,266'/>
		<Face Indices='275,274,266'/>
		<Face Indices='266,267,275'/>
		<Face Indices='267,268,275'/>
		<Face Indices='276,275,268'/>
		<Face Indices='276,269,284'/>
		<Face Indices='277,284,269'/>
		<Face Indices='269,270,278'/>
		<Face Indices='278,277,269'/>
		<Face Indices='270,271,278'/>
		<Face Indices='279,278,271'/>
		<Face Indices='271,272,279'/>
		<Face Indices='280,279,272'/>
		<Face Indices='272,273,280'/>
		<Face Indices='281,280,273'/>
		<Face Indices='273,274,281'/>
		<Face Indices='282,281,274'/>
		<Face Indices='274,275,283'/>
		<Face Indices='283,282,274'/>
		<Face Indices='275,276,284'/>
		<Face Indices='284,283,275'/>
		<Face Indices='284,277,292'/>
		<Face Indices='285,292,277'/>
		<Face Indices='277,278,286'/>
		<Face Indices='286,285,277'/>
		<Face Indices='278,279,286'/>
		<Face Indices='287,286,279'/>
		<Face Indices='279,280,287'/>
		<Face Indices='288,287,280'/>
		<Face Indices='280,281,288'/>
		<Face Indices='289,288,281'/>
		<Face Indices='281,282,290'/>
		<Face Indices='290,289,281'/>
		<Face Indices='282,283,291'/>
		<Face Indices='291,290,282'/>
		<Face Indices='283,284,292'/>
		<Face Indices='292,291,283'/>
		<Face Indices='292,285,300'/>
		<Face Indices='293,300,285'/>
		<Face Indices='285,286,294'/>
		<Face Indices='294,293,285'/>
		<Face Indices='286,287,295'/>
		<Face Indices='295,294,286'/>
		<Face Indices='287,288,296'/>
		<Face Indices='296,295,287'/>
		<Face Indices='288,289,296'/>
		<Face Indices='297,296,289'/>
		<Face Indices='289,290,298'/>
		<Face Indices='298,297,289'/>
		<Face Indices='290,291,298'/>
		<Face Indices='299,298,291'/>
		<Face Indices='291,292,299'/>
		<Face Indices='300,299,292'/>
		<Face Indices='300,293,307'/>
		<Face Indices='301,307,293'/>
		<Face Indices='293,294,301'/>
		<Face Indices='302,301,294'/>
		<Face Indices='294,295,303'/>
		<Face Indices='303,302,294'/>
		<Face Indices='296,297,304'/>
		<Face Indices='297,298,305'/>
		<Face Indices='305,304,297'/>
		<Face Indices='298,299,305'/>
		<Face Indices='306,305,299'/>
		<Face Indices='299,300,306'/>
		<Face Indices='307,306,300'/>
		<Face Indices='307,301,314'/>
		<Face Indices='308,314,301'/>
		<Face Indices='301,302,308'/>
		<Face Indices='309,308,302'/>
		<Face Indices='302,303,310'/>
		<Face Indices='310,309,302'/>
		<Face Indices='304,305,312'/>
		<Face Indices='312,311,304'/>
		<Face Indices='305,306,312'/>
		<Face Indices='313,312,306'/>
		<Face Indices='306,307,313'/>
		<Face Indices='314,313,307'/>
		<Face Indices='314,308,319'/>
		<Face Indices='308,309,315'/>
		<Face Indices='309,310,316'/>
		<Face Indices='316,315,309'/>
		<Face Indices='311,312,318'/>
		<Face Indices='318,317,311'/>
		<Face Indices='312,313,318'/>
		<Face Indices='315,316,320'/>
		<Face Indices='321,320,316'/>
		<Face Indices='317,318,323'/>
		<Face Indices='323,322,317'/>
		<Face Indices='320,321,325'/>
		<Face Indices='322,323,325'/>
		<Face Indices='107,260,259'/>
		<Face Indices='259,125,107'/>
		<Face Indices='122,261,260'/>
		<Face Indices='260,107,122'/>
		<Face Indices='122,106,262'/>
		<Face Indices='261,122,262'/>
		<Face Indices='4,5,265'/>
		<Face Indices='264,4,265'/>
		<Face Indices='5,7,266'/>
		<Face Indices='265,5,266'/>
		<Face Indices='14,267,266'/>
		<Face Indices='266,7,14'/>
		<Face Indices='267,14,268'/>
		<Face Indices='125,259,268'/>
		<Face Indices='14,125,268'/>
		<Face Indices='184,205,256'/>
		<Face Indices='184,206,205'/>
		<Face Indices='184,231,206'/>
		<Face Indices='161,256,183'/>
		<Face Indices='161,183,159'/>
		<Face Indices='161,184,256'/>
		<Face Indices='160,161,159'/>
		<Face Indices='185,257,161'/>
		<Face Indices='163,185,161'/>
		<Face Indices='163,161,162'/>
		<Face Indices='257,184,161'/>
		<Face Indices='257,208,184'/>
		<Face Indices='209,233,184'/>
		<Face Indices='208,209,184'/>
		<Face Indices='4,263,2'/>
		<Face Indices='263,4,264'/>
		<Face Indices='263,13,2'/>
		<Face Indices='263,104,13'/>
		<Face Indices='263,106,104'/>
		<Face Indices='262,106,263'/>
		<Face Indices='295,296,304'/>
		<Face Indices='304,303,295'/>
		<Face Indices='303,304,311'/>
		<Face Indices='311,310,303'/>
		<Face Indices='315,319,308'/>
		<Face Indices='319,315,320'/>
		<Face Indices='310,311,316'/>
		<Face Indices='317,316,311'/>
		<Face Indices='319,318,313'/>
		<Face Indices='313,314,319'/>
		<Face Indices='320,324,319'/>
		<Face Indices='324,320,325'/>
		<Face Indices='322,321,316'/>
		<Face Indices='321,322,325'/>
		<Face Indices='316,317,322'/>
		<Face Indices='318,319,324'/>
		<Face Indices='324,323,318'/>
		<Face Indices='323,324,325'/>
	</Mesh.Faces>
    </Mesh>
    <Sphere Position="-1.0,-1.0,0" Radius="0.5">
      <Sphere.Material>
       <Matte AmbientCoeff="0.5" DiffuseCoeff="0.5" Color="0,1,0" Shadows="True"/>
      </Sphere.Material>
    </Sphere>
    <Plane Position="0,-1.5,0" Direction="0,1,0" Shadows="True">
      <Plane.Material>
        <Matte AmbientCoeff="0.5" DiffuseCoeff="0.5" Color="1,1,1" Shadows="True"/>
      </Plane.Material>
    </Plane>
  </Objects>
</World></textarea>
                    </div>
                </td>
                <td style="width: 200px">
                    <div style="width: 200px; height: 575px; overflow: auto; border-style: solid; border-width: 1px">
                        <div id="popup" style="position: absolute; width: 200px; height: 600px; background: Gray;
                            visibility: hidden;">
                            <div style="margin: auto; margin-top: 250px; width: 200px; background: White">
                                Url:
                                <input id="urlBox" type="text" style="width: 100%" />
                                Unique Name:
                                <input id="nameBox" type="text" style="width: 100%" />
                                <input id="subButton" type="button" value="Add" style="float: right" onclick="addImage()" />
                                <input id="addButton" type="button" value="Close" style="float: right" onclick="closePopup()" />
                            </div>
                        </div>
                        <div id="list" style="width: 200px; height: 575px; overflow: auto">
                        </div>
                    </div>
                    <div style="width: 200px; height: 25px; overflow: auto">
                        <input type="button" value="Add Image" style="float: right;" onclick="showPopup()" />
                    </div>
                </td>
            </tr>
        </table>
        <div style="width: 800px; height: 25px; margin-top: 10px">
            <div id="progressContainer" style="width: 400px; height: 18px; margin-top: 3px; float: left;
                background: #C1C9C5">
                <div id="progressDiv" style="width: 0px; height: inherit; font-size: small; text-align: center;
                    background-image: url('../Images/progress.png'); background-repeat: repeat-x;
                    background-position: left center;">
                    0%
                </div>
            </div>
            <div id="status" style="margin-left: 5px; float: left">
            </div>
            <input type="button" id="renderButton" value="Render" onclick="render()" style="float: right" />
        </div>
        <div id="slide" style="height: 150px; width: 800px; overflow: hidden">
        </div>
        <div id="er" style="color: Red; font-size: large; overflow: auto">
        </div>
        <script type="text/javascript">
            var sessionId;
            var editor;
            var helper = "HHelper.ashx?";

            function prepareSplitter() {
                $().ready(function () {
                    $("#editorContainer").splitter({ type: "h" });
                });
            }

            //      function prepareXmlWords() {
            //          CodeMirror.xmlHints['<'] = [
            //          'World'
            //      ];
            //          CodeMirror.xmlHints['<World '] = [
            //          'Tracer'
            //      ];
            //          CodeMirror.xmlHints['<World><'] = [
            //          'World.ViewPlane', 'World.AmbientLight'
            //      ];
            //          CodeMirror.xmlHints['<World.ViewPlane '] = [
            //          'Width', 'Height', 'Samples', 'PixelSize', 'MaxDepth', 'Sampler'
            //      ];
            //      }

            function prepareXmlEditor() {
                editor = CodeMirror.fromTextArea(document.getElementById("code"), {
                    mode: 'text/html',
                    lineNumbers: true,
                    autoCloseTags: true
                });
                // prepareXmlWords();
                $("#textContainer").resize(function (e) {
                    editor.setSize(800, $("#textContainer").height());
                });
            }

            function prepareSlider() {
                var xml = new XMLHttpRequest();
                xml.open("GET", "Scripts/Samples/samples.xml", false);
                xml.send();
                var xmldoc = xml.responseXML;
                var root = xmldoc.getElementsByTagName("Sample");
                for (var i = 0; i < root.length; i++) {
                    var ele = root[i];
                    insertSlide(ele.getAttribute("src"), ele.getAttribute("img"), ele.getAttribute("head"));
                }
            }

            function insertSlide(src, img, head) {
                var d = document.createElement('div');
            }

            function getSessionId() {
                $.get(helper + "cmnd=getUniqueSessionID", function (data) {
                    sessionId = data;
                });
            }

            function clearError() {
                $("#er").text("");
                $("#er").height = "0px";
            }

            function writeError(err) {
                $("#er").text("Error: " + err);
                $("#er").height = "50px";
            }

            function compile(fi) {
                clearError();
                $.post(helper + "cmnd=compile&id=" + sessionId, { file: fi }, function (data) {
                    if (data == "s") {
                        startProgress();
                        $.post(helper + "cmnd=render&id=" + sessionId, function (d) {
                        });
                    }
                    else {
                        writeError(data);
                    }
                });
            }

            function startProgress() {
                $("#status").text("Rendering...");
                var timer = setInterval(function () {
                    $.get(helper + "cmnd=progress&id=" + sessionId, function (d) {
                        var n = parseFloat(d);
                        progressDiv.style.width = (n * 4) + "px";
                        $("#progressDiv").text(d + "%");
                        updateImage();
                        if (n == 100) {
                            $("#status").text("Render completed.");
                            updateImage();
                            clearInterval(timer);
                        }
                        else {
                            $.get(helper + "cmnd=pixels&id=" + sessionId, function (d) {
                                $("#status").text("Rendering... " + d + " pixels");
                            });
                        }
                    });

                }, 1000);
            }

            function updateImage() {
                image.src = helper + "cmnd=getImage&type=render&id=" + sessionId + "#" + Math.random();
            }

            function render() {
                compile(editor.getValue());
            }

            function showPopup() {
                popup.style.visibility = 'visible';
            }

            function closePopup() {
                popup.style.visibility = 'hidden';
            }

            function addImage() {
                if (nameBox.value != "") {
                    if (urlBox.value != "") {
                        if (!hasName(nameBox.value)) {
                            $.post(helper + "cmnd=addImage&type=url&id=" + sessionId + "&name=" + nameBox.value, { file: urlBox.value }, function (data) {
                                if (data == "s") {
                                    addNewImage(nameBox.value);
                                    closePopup();
                                }
                                else {
                                    alert("Error: " + data);
                                }
                            });
                        }
                        else {
                            alert("The name " + nameBox.value + " already exists");
                        }

                    }
                    else {
                        alert("Please specify a Url or chose a file");
                    }
                }
                else {
                    alert("Please enter a name.");
                }
            }

            function hasName(name) {
                $.get(helper + "cmnd=checkName&id=" + sessionId + "&name=" + name, function (data) {
                    if (data == "t") {
                        return true;
                    }
                    else {
                        return false;
                    }
                });
            }

            function addNewImage(im) {
                var div = "<div><img src='" + helper + "cmnd=getImage&id=" + sessionId + "&type=imgs&name=" + im + "'/><br /><center>Name: " + im + "</center></div>";
                document.getElementById("list").innerHTML += div;
            }

            try {
                getSessionId();
                prepareSplitter();
                prepareXmlEditor();
                prepareSlider();
            }
            catch (err) {
                alert(err);
            }
        </script>
    </div>
</asp:Content>

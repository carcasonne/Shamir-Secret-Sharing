﻿// See https://aka.ms/new-console-template for more information
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Numerics;
using Utilities;
using Utilities.Extensions;

var debug = false;
var random = new Random();

// string in single line to avoid \n\r and stuff like that
// values given in the "secret_sharing" file
string field_hex =  @"00b44a0bc6303782b729a7f9b44a3611b247ddf1e544f8b1420e2aae976003219175461d2bd76e64ba657d7c9dff6ed7b17980778ec0cbf75fc16e52463e2d784f5f20c1691f17cdc597d7a5141080809a38c635b2a62082e310aa963ca15953894221ad54c6b30aea10f4dd88a66c55ab9c413eae49c0b28e6a3981e0021a7dcb0759af34b095ce3efce78938f2a2bed70939ba47591b88f908db1eadf237a7a7100ac87130b6119d7ae41b35fd27ff6021ac928273c20f0b3a01df1e6a070b8e2e93b5220ad02104000c0c1e82e17fd00f6ac16ef37c3b6153d348e470843a84f25473a51f040a42671cd94ffc989eb27fd42b817f8173bfa95bdfa17a2ae22fd5c89dab2822bcc973b5b90f8fadc9b074cca8f9365b1e8994ff0bda48b1f7498cce02d4e794915f8a4208de3eaf9fbff5";
string share1_hex = "009aca2ca92b1e95bfad348c9014c6adc00d18d29fd5f891d0837c9fe18db35cc28d654114d6159dd6664405ead5277e24bcdbda9984c28e3b810377744f420e0fc52ada1cafb328f6aaa9656d31c73b98af938f784d3d611e7e6f124119e948745d15c829d794f47eb76b3fdfc16824ff6d46bcf534b1a2d8b3f2de97250f3f3b16f87dba41d54b127c10b2b44d7d54c00d89ce91b590c065cc210dd841c8460a7ac535fb0a6e26b312695c2635b5a8d311fb4473bee791f35f92dc70524954f5f60b98352e4d63b1f8c7357c1e52d696f67b2ff14a988a1691352fa0d3401d7d4f806598b651e5e21bc133ad2340a327cef3d47ef2bddc386a98dabcebaa814becb09ce3d8032d933664af6b495849030b03e6da89d971e5a45f19ecdef254e3549ba5af53ed18b7a013c81b154a06eb76";
string share2_hex = "2131de2d5f973cd76289fe88dfe2cd9e8196b7cfb26b8793fb3afa5bc0b965441878cf300f1a39db3525dc4881a4b4654bf648b6b812e202d0ea3e7654fe02cadc68f72978093eee3731ecd0ff1f7b32e52de1d9e7575315112cbe693205a0890ae8a2fe33610e9097ce3c7f819113315686179c226df4bea68cca4e466fcf4ca343fa60019ca4914acedc84df0e12c8ad4a3d5590f51e321a7f3528dfb7939241e1377c632e913813ac02757d899a19c10900fdd4ed24d1affc0249b3fdd93df4de4f229dffa039af1589f5fe87cf7594cc8379f364a64334ecc165aa650cf81edc2fd1791b95128926c9be25b94e60fad290d3f5ef79403025d2ead3ae2f77cc054f4dbc3a5183d31eeb8626c00512769ad03092f0b2f256fc2b2f8b6250659e656c1569d96b164d7e0908f84e99e728";
string share4_hex = "86f6700424c9527db41c2403b69ea80fb2902fa720983929691c81b88a7c5bf830b5153d749327b9a80422f2a61cd51b5c5c7568a059ab38660f135e05ba62a5e6014d3f0ce4b0522b81df3231b111c06199faa7cff0387981dd35cc62ea6a451d2c0b12b39f6676adbe82058c3cb4847856477f8f93962c4c10fc5fb62906d95bcb7aad486b564d8c3f50cbeb2d21b6aa0e8e46d03b9ba75b1adf4fa9d41ca32e977fbdafadaa4f38f20020d30c5f26e30ac1ad56993bc246c06fd0bcbb12e540b1fa6292ba403f45f03f9cf446ac4a37bb4aa180a3f262466d3129fccca216c1521143cabc818dda793ce26b3b700c3b15cd648193771e797863f7782e6b460d593c92b15a90c843d9d09f65ff1b25cda9b758baa456fe15";

// Converting to BigInteger
var field = BigInteger.Parse(field_hex, NumberStyles.AllowHexSpecifier);
var share1 = BigInteger.Parse(share1_hex, NumberStyles.AllowHexSpecifier);
var share2 = BigInteger.Parse(share2_hex, NumberStyles.AllowHexSpecifier);
var share4 = BigInteger.Parse(share4_hex, NumberStyles.AllowHexSpecifier);
if(debug)
{
    Console.WriteLine($"Field: {field}");
    Console.WriteLine($"Share 1: {share1}");
    Console.WriteLine($"Share 2: {share2}");
    Console.WriteLine($"Share 4: {share4}");
}

Console.WriteLine("-------------------- RETRIEVING THE SECRET FROM 3 SHARES FROM PROJECT DESCRIPTION ------------------------");

// Creating the points; Assuming share_i is equal to Point(i, share_i)
Point p1 = new Point(1, share1);
Point p2 = new Point(2, share2);
Point p4 = new Point(4, share4);
Point[] points = {p1, p2, p4};

// Retrieves the secret
var secret = Polynomial.GetBaseValueFromPoints(points, 2);
Console.WriteLine($"The secret is: {secret}");

Console.WriteLine("-------------------- TESTING OUR IMPLEMENTATION OUT WITH OWN VALUES ------------------------");
//Testing that our implementation works
BigInteger new_secret = random.NextInt64();
var neededShares = 3; 
Console.WriteLine($"Randomly generated secret: {new_secret}, shares needed to retrieve secret: {neededShares}");
var polynomial = new Polynomial(new_secret, neededShares - 1, field);
Point new_point1 = new Point(1, polynomial.GetYFromX(1));
Point new_point2 = new Point(2, polynomial.GetYFromX(2));
Point new_point3 = new Point(3, polynomial.GetYFromX(3));
Point[] new_points = {new_point1, new_point2, new_point3};

var new_reconstruction = Polynomial.GetBaseValueFromPoints(new_points, 2);
Console.WriteLine($"Secret should be {new_secret}; Reconstructed Secret is = {new_reconstruction}");
Console.WriteLine($"Is equal: {new_secret == new_reconstruction}");






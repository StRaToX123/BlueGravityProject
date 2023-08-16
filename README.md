# BlueGravityProject
<br />
Pavle Marinkovic<br /> 
marinkovic.pavle@gmail.com <br />
<br />
Controls: <br />
    - Move : WASD<br />
    - Interact : e<br />
    - Back : escape<br />
<br />
# Overview:
<br />
![BlueGravityProject](https://github.com/StRaToX123/BlueGravityProject/assets/26925590/c6393125-170a-414e-8bd3-f49930200731)
<br />
<br />
The project contains the following features, all the scripting logic mentioned here was writen during the making of this project: <br />
    - A pixalated map, reminiscent of “Stardew Valley” <br />
    - A character controller, the player can walk around the world and interact <br />
    - Merchant NPCs. I have created two variants. The player can interact with them once they walk up to them, indicated by a pop up button prompt above the merchant <br />
    - Merchant interaction options menu. <br />
    - Transaction menu. This menu doubles for both the buy menu and a sell menu for interacting with merchants. <br />
    - A couple of items represented via scriptable objects for the merchants to use <br />
    - A couple of custom made images (Button Prompt Images, Menu Backgrounds) <br />
    <br />
Used Free Asset Packs: <br />
    - 2D Mega Pack, made by “Brackeys” <br />
    - Mighty Heroes (Rogue) 2D <br />
    - Cainos Pixel Art Top Down <br />
    <br />
    <br />
# Merchants and the Transaction Menu:
   <br />
There are two merchants present in the demo (Merchant Cat, Merchant Rogue), both inherit from a base class Merchant.   <br /> 
This allows them to have basic interactability with the player as well as offer a selectable list of options upon    <br />
interacting with them. Each merchant then, alters and implements different options (i.e. the merchant rogue has a    <br />
talk options while the merchant cat does not).     <br />
The transaction menu features the ability to scroll through an arbitrarily long list of items and call function    <br />
callbacks whenever an item is sold, allowing for an extendable system where, each merchant can implement custom logic   <br />
(i.e. accepting only scertain items, or having custom dialog for some items). Added some quick animations to the UI    <br />
in order to smooth out how things look.    <br />

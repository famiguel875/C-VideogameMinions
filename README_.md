# C#VideogameMinions

## UML

### Diagrama de flujo UML que muestra el planteamiento del programa

<img src="img\UMLVideogameMinions.png" alt="UML" width="1470">

## MinionDelegate

### Delegate público utilizado para invocar a los minions

<img src="img\MinionDelegate.png" alt="MinionDelegate" width="1470">

## IItem

### Interfaz item utilizada para funcionalidades comunes 

<img src="img\IItem.png" alt="IItem" width="1470">

## Character

### Clase abstracta que asienta parámetros comunes a sus clases hijas

<img src="img\Character.png" alt="Character" width="1470">

## PartyMember

### Clase para miembros principales del grupo, estos tienen acceso a inventario e invocar minions

<img src="img\PartyMember.png" alt="PartyMember" width="1470">

## Minion

### Clase para minions, define sus propiedades básicas

<img src="img\Minion.png" alt="Minion" width="1470">

## Weapon

### Clase abstracta que asienta propiedades comunes para las armas

<img src="img\Weapon.png" alt="Weapon" width="1470">

## Sword

### Clase hija que hereda de Weapon

<img src="img\Sword.png" alt="Sword" width="1470">

## Axe

### Clase hija que hereda de Weapon

<img src="img\Axe.png" alt="Axe" width="1470">

## SwordMagical

### Clase hija que hereda de Weapon, invoca un minion

<img src="img\SwordMagical.png" alt="SwordMagical" width="1470">

## AxeMagical

### Clase hija que hereda de Weapon, invoca un minion

<img src="img\AxeMagical.png" alt="AxeMagical" width="1470">

## Protection

### Clase abstracta que asienta propiedades comunes para los objetos de protección

<img src="img\Protection.png" alt="Protection" width="1470">

## Shield

### Clase hija que hereda de Protection

<img src="img\Shield.png" alt="Shield" width="1470">

## Helmet

### Clase hija que hereda de Protection

<img src="img\Helmet.png" alt="Helmet" width="1470">

## ShieldMagical

### Clase hija que hereda de Protection, invoca un minion

<img src="img\ShieldMagical.png" alt="ShieldMagical" width="1470">

## MinionCreator

### Clase que crea los minions a través del delegate MinionDelegate

<img src="img\MinionCreator.png" alt="MinionCreator" width="1470">

## Program

### Programa principal que ejecuta los tests manuales

<img src="img\Program.png" alt="Program" width="1470">
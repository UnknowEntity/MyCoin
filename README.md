# MyCoin

#### Must first sep up server.
#### Read server repo readme for instruction.
##### Change server host name in file XmlManager.cs line 55 before build.
```c#
root.Add(new XElement("Http", "http://localhost:3000/"));
```
##### Must have total of 3 wallet before making transaction. (create 3 wallet debug folder and run the .exe file and wait for the confirm).
##### Must create total of 2 transaction before server starting to mine (because reward transaction of node for mining count as 1 transaction).
##### To create transaction:
+ Click New Address button on the "to" wallet.
+ Copy and Paste Address and Pubkey to the "from" wallet.
+ Write amount to tranfer.
+ Click Add Address.
+ Click create transaction.

##### Setting to get server list, change current server, check server.
##### Transaction list to check all transaction make in that wallet(only the "from" wallet).

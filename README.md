# Email-Encryption
  In this project I built an outlook add-in to encrypt and verify e-mail messages (end to end encryption like TLS).</br>
  The encryption was done using RSA and the signing was done using SHA-256 and RSA(All the encryption and decryption was done in the TEE).</br>
<h1>so how it's works?</h1></br>
When you download the email add-in:</br>
In the TEE was generated pair of keys: private(d: 2048 bit) and public key(mod: 2048 bit, e:65537). The TEE also store the keys in it's memory and sends the public key to the host.</br>
![key](https://user-images.githubusercontent.com/72938168/182716429-889dbd34-c217-47c4-ad81-db10e7853856.png)


  

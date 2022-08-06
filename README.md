# Email-Encryption
  In this project I built an outlook add-in to encrypt and verify e-mail messages (end to end encryption like TLS).</br>
  The encryption was done using RSA and the signing was done using SHA-256 and RSA(All the encryption and decryption was done in the TEE).</br>
<h1>So How it Works?</h1>
<h3>In the beginning...</h3>
When you download the email add-in:</br>
In the TEE was generated pair of keys: private(d: 2048 bit) and public key(mod: 2048 bit, e:65537). The TEE also stores the keys in it's memory and sends the public key to the host.</br>
<img src="https://user-images.githubusercontent.com/72938168/182718570-219bba3c-8160-4224-bbd7-9ba1fafbd8f4.png"></br>
The host sends the public key to the CA Server (on SslStream using TlsClient class found in StreamSupport).</br>
The CA signs on the host's public key, stores it and returns the signing + the original key back to host.</br>
Now the user can sign and encrypt emails as well as decrypt and verify emails sent to him.</br>
<h3>Encrypt and sign message</h3></br>
The user writes a normal new message in outlook... but when the user clicks send - surprise:</br>
<img src="https://user-images.githubusercontent.com/72938168/182726179-95cc0e61-a64f-4768-978f-ac11bcdaaa98.png" title="The user writes a normal message">
We intercept outlook event (item send) and take the email recipient address, subject and body from the message.</br>
<img src="https://user-images.githubusercontent.com/72938168/182722046-9ab8f607-e6a6-4079-b811-2e420941e458.png">
<!-- img src="https://user-images.githubusercontent.com/72938168/182726179-95cc0e61-a64f-4768-978f-ac11bcdaaa98.png" -->
They are all sent to the host which asks from the CA the recipient public key (via their email and again the communication between them is secure).</br>
The host sends the recipient's public key and the email content for the TEE. The TEE encrypts the email content with the recipient's public key and sign on the encrypted message with the client's private key and sends the result back to the host which sends it back to outlook add-in.</br>
The result is attached to the original email as a binary file and the user can send it.</br>
<img src="https://user-images.githubusercontent.com/72938168/182726424-72062992-5ff7-4190-88b2-066776670d5c.png">
<h3>Decrypt and Verify</h3>
So this is what the message we sent earlier looks like on the recipient's side:
<img src="https://user-images.githubusercontent.com/72938168/182726807-95fbdb2d-eb80-462b-b17e-429c81d9d0ce.png">
The recipient will need to click on the Add-ins option in the task pan and then click on the Decrypt button.</br>
The message will go from the add-on through the host (which asks the CA for the sender's public key) to the recipient's TEE, where the signature will be verified and the original message will be decrypted and then the add-in will display the decrypted message like this:
<img src="https://user-images.githubusercontent.com/72938168/183265324-1aac8f84-7ba8-4ba2-8747-73fee89cd5ef.png">
















  

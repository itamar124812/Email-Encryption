package EmailEncryption;

import com.intel.util.*;
import com.intel.crypto.SymmetricSignatureAlg;
import com.intel.crypto.Random;
import com.intel.crypto.RsaAlg;
import com.intel.crypto.HashAlg;
import com.intel.langutil.ArrayUtils;
//
// Implementation of DAL Trusted Application: EmailEncryption 
//
// **************************************************************************************************
// NOTE:  This default Trusted Application implementation is intended for DAL API Level 7 and above
// **************************************************************************************************

public class EmailEncryption extends IntelApplet {
    RsaAlg rsaAlg=RsaAlg.create();
    short modSize=(short)256;
    short keySize=(short)32;
    short pesize=(short)4;
    short dsize=(short)256;
    int segLength=240;

	public byte [] generateKeys()
	{
		rsaAlg.setHashAlg(rsaAlg.HASH_TYPE_SHA256);
		rsaAlg.setPaddingScheme(rsaAlg.PAD_TYPE_PKCS1);
		rsaAlg.generateKeys(modSize);		
		byte [] pb=new byte[rsaAlg.getModulusSize()+rsaAlg.getPublicExponentSize()];
		byte [] pv=new byte[rsaAlg.getModulusSize()+rsaAlg.getPublicExponentSize()+rsaAlg.getPrivateExponentSize()];
		rsaAlg.getKey(pb,(short) 0, pb, rsaAlg.getModulusSize());
		rsaAlg.getKey(pv,(short) 0, pv, rsaAlg.getModulusSize(),pv,(short)(rsaAlg.getModulusSize()+rsaAlg.getPublicExponentSize()));
		FlashStorage.writeFlashData(0, pv, 0, pv.length);
		return pb;	
	}
	public byte[] setkeys()
	{
		if(FlashStorage.getFlashDataSize(0)!=0) {
		rsaAlg.setHashAlg(rsaAlg.HASH_TYPE_SHA256);
		rsaAlg.setPaddingScheme(rsaAlg.PAD_TYPE_PKCS1);
		byte [] pb=new byte[modSize+pesize];
		byte [] pv=new byte[modSize+pesize+dsize];
		FlashStorage.readFlashData(0, pv, 0);
		rsaAlg.setKey(pv, (short)0, modSize, pv,modSize ,pesize, pv,(short)(pesize+ modSize),(short)(pv.length-(pesize+ modSize)));
		rsaAlg.getKey(pb,(short) 0, pb, rsaAlg.getModulusSize());
		return pb;
		}
		else 
		{
			return generateKeys();
		}
	
	}
	
	public byte [] encrypteMessage(byte [] message,byte [] pb)
	{
		RsaAlg Encreptor=RsaAlg.create();
		byte [] encrypted=new byte[modSize];
		Encreptor.setHashAlg(rsaAlg.HASH_TYPE_SHA256);
		Encreptor.setPaddingScheme(rsaAlg.PAD_TYPE_PKCS1);		
		Encreptor.setKey(pb, (short) 0, modSize, pb, modSize,(short)(pb.length-modSize));
		if(message.length>256)
		{
			
			byte[] seg=new byte[(message.length/segLength + 1)*256];
			int index=0;
			short secondIndex=0;
			while(index<message.length)
			{
				byte [] segment=new byte[segLength];
				if(index+segment.length>message.length)
					ArrayUtils.copyByteArray(message, (short)index, segment, (short)0, (short)(message.length-index));
				else ArrayUtils.copyByteArray(message, (short)index, segment, (short)0, (short)segment.length);
				Encreptor.encryptComplete(segment, (short)0, (short)segment.length, encrypted, (short)0);
				ArrayUtils.copyByteArray(encrypted, (short)0, seg, (short)secondIndex, (short)encrypted.length);
				secondIndex+=256;
				index+=segLength;
			}
			encrypted=seg;
		}
		else 
		{
			Encreptor.encryptComplete(message, (short)0, (short)message.length, encrypted, (short)0);
		}
		byte [] sig=sign(encrypted);
		byte [] result=new byte[encrypted.length+sig.length];
		ArrayUtils.copyByteArray(encrypted, (short)0, result, (short)0, (short)encrypted.length);
		ArrayUtils.copyByteArray(sig, (short)0, result, (short)encrypted.length, (short)sig.length);
		return result;
	}
	
	public boolean verify(byte[]message,byte [] sig,byte[]pb)
	{
		RsaAlg verifier=RsaAlg.create();
		verifier.setHashAlg(rsaAlg.HASH_TYPE_SHA256);
		verifier.setPaddingScheme(rsaAlg.PAD_TYPE_PKCS1);		
		verifier.setKey(pb, (short) 0, modSize, pb, modSize,(short)(pb.length-modSize));
		return verifier.verifyComplete(message,(short) 0,(short)message.length ,sig, (short)0, (short) sig.length);
		
	}
	public byte[] decrypt(byte[] message)
	{
		byte [] result=new byte [2000];
		if(message.length>256)
		{
			int index=0;
			int secoundIndex=0;
		    while(secoundIndex<message.length) {
		    byte [] segment=new byte[modSize];
		    ArrayUtils.copyByteArray(message, (short)secoundIndex, segment, (short)0, (short)segment.length);
			rsaAlg.decryptComplete(segment, (short)0, (short)segment.length,result, (short)index);
			index+=segLength;
			secoundIndex+=256;
		    }
		    return result;		
		}
		rsaAlg.decryptComplete(message, (short)0, (short)message.length,result, (short)0);
		return result;
		
	}
	public void setCApb(byte [] pb)
	{
		
	}
	/**
	 * This method will be called by the VM when a new session is opened to the Trusted Application 
	 * and this Trusted Application instance is being created to handle the new session.
	 * This method cannot provide response data and therefore calling
	 * setResponse or setResponseCode methods from it will throw a NullPointerException.
	 * 
	 * @param	request	the input data sent to the Trusted Application during session creation
	 * 
	 * @return	APPLET_SUCCESS if the operation was processed successfully, 
	 * 		any other error status code otherwise (note that all error codes will be
	 * 		treated similarly by the VM by sending "cancel" error code to the SW application).
	 */
	public int onInit(byte[] request) {
		DebugPrint.printString("Hello, DAL!");
		return APPLET_SUCCESS;
	}
	
	/**
	 * This method will be called by the VM to handle a command sent to this
	 * Trusted Application instance.
	 * 
	 * @param	commandId	the command ID (Trusted Application specific) 
	 * @param	request		the input data for this command 
	 * @return	the return value should not be used by the applet
	 */
	public int invokeCommand(int commandId, byte[] request) {
		
		DebugPrint.printString("Received command Id: " + commandId + ".");
		if(request != null)
		{
			DebugPrint.printString("Received buffer:");
			
		}
		byte[] myResponse = { 'O', 'K' };
		switch (commandId)
		{
		case 1:
		{
			myResponse=setkeys();
			break;
		}
		case 2:
		{
			int messageLength=ByteArrayToInt(request,0);
			byte []message=new byte[messageLength];
			byte [] pb=new byte[request.length-(messageLength+4)];
			ArrayUtils.copyByteArray(request, (short)4, message, (short)0, (short)messageLength);
			ArrayUtils.copyByteArray(request, (short)(4+messageLength), pb, (short)0, (short)request.length-(messageLength+4));
			myResponse=encrypteMessage(message, pb);
			break;
		}
		case 3:
		{
			int messageLength=ByteArrayToInt(request,0);
			short realmsg=(short)(messageLength-rsaAlg.getSignatureLength());
			byte []message=new byte[realmsg];
			byte [] pb=new byte[request.length-(messageLength+4)];
			byte [] sig=new byte[rsaAlg.getSignatureLength()];
			ArrayUtils.copyByteArray(request, (short)4, message, (short)0, realmsg);
			ArrayUtils.copyByteArray(request, (short)(realmsg+4),sig,(short)0,sig.length);
			ArrayUtils.copyByteArray(request, (short)(4+messageLength), pb, (short)0, (short)request.length-(messageLength+4));
			if(verify(message, sig, pb))
				myResponse=decrypt(message);
			else {
				setResponseCode(-1);
				myResponse = pb;
			}		
			break;
		}
		default:
		{
			break;
		}
		}
		

		/*
		 * To return the response data to the command, call the setResponse
		 * method before returning from this method. 
		 * Note that calling this method more than once will 
		 * reset the response data previously set.
		 */
		setResponse(myResponse, 0, myResponse.length);

		/*
		 * In order to provide a return value for the command, which will be
		 * delivered to the SW application communicating with the Trusted Application,
		 * setResponseCode method should be called. 
		 * Note that calling this method more than once will reset the code previously set. 
		 * If not set, the default response code that will be returned to SW application is 0.
		 */
		setResponseCode(0);

		/*
		 * The return value of the invokeCommand method is not guaranteed to be
		 * delivered to the SW application, and therefore should not be used for
		 * this purpose. Trusted Application is expected to return APPLET_SUCCESS code 
		 * from this method and use the setResposeCode method instead.
		 */
		return APPLET_SUCCESS;
	}

	/**
	 * This method will be called by the VM when the session being handled by
	 * this Trusted Application instance is being closed 
	 * and this Trusted Application instance is about to be removed.
	 * This method cannot provide response data and therefore
	 * calling setResponse or setResponseCode methods from it will throw a NullPointerException.
	 * 
	 * @return APPLET_SUCCESS code (the status code is not used by the VM).
	 */
	public int onClose() {
		DebugPrint.printString("Goodbye, DAL!");
		return APPLET_SUCCESS;
	}
	private int ByteArrayToInt(byte[] bytes,int i) {
        return ((bytes[i+3] & 0xFF) << 24) |
                ((bytes[i+2] & 0xFF) << 16) |
                ((bytes[i+1] & 0xFF) << 8) |
                ((bytes[i] & 0xFF) << 0);
    }
	private byte[] generateRandom(int size) {	
			byte[] result = new byte[size];
			Random.getRandomBytes(result, (short) 0, (short) ((short) size - 1));
			DebugPrint.printBuffer(result);
			return result;
	}
	private byte[] sign(byte [] message)
	{
		byte[] sig=new byte[rsaAlg.getSignatureLength()];
		rsaAlg.signComplete(message, (short)0, (short) message.length,sig, (short)0);
		return sig;
	}
	

}

import java.io.File;
import java.io.FileNotFoundException;
import java.util.*;

public class ECB {

    private static int DELTA = 0x9e3779b9;
    private int sum;
    private int[] key;

    public ECB(int[] keyAdd) throws FileNotFoundException{
        File file = new File("key.txt");
        if (file.exists()) {
            Scanner in = new Scanner(file);
            String keycode;
            keycode = in.nextLine();
            if(keycode.length() < 4) {
                System.out.println("Key has to be length of 4");
                System.exit(0);
            }
            key = new int[4];
            for(int i = 0; i < 4; i++) {
                key[i] = keycode.charAt(i);
            }
            in.close();
        }
        else {
            key = new int[4];
            for(int i = 0; i < 4; i++) {
                key[i] = keyAdd[i];
            }
        }

    }

    public int[] encrypt(int[] plainText){
        if(key == null){
            System.out.println("Error : KEY NULL.");
            System.exit(0);
        }

        int left = plainText[0];
        int right = plainText[1];

        sum = 0;

        for(int i=0; i<32;i++){
            sum += DELTA;
            left += ((right << 4) + key[0]) ^ (right+sum) ^ ((right >> 5) + key[1]);
            right += ((left << 4) + key[2]) ^ (left+sum) ^ ((left >> 5) + key[3]);
        }

        int block[] = new int[2];
        block[0] = left;
        block[1] = right;

        return block;
    }
}
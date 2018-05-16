import java.io.*;

//Jan Bienias
//238201
//Vigenere

public class Main{
    public static void prepareFiles(){
        File orig = new File("orig.txt");
        File plain = new File("plain.txt");
        String linetext = "", all = "";
        StringBuilder sB = new StringBuilder(all);
        try{
            try{
                try{
                    Reader reader = new InputStreamReader(new FileInputStream(orig),"ASCII");
                    BufferedReader in = new BufferedReader(reader);
                    Writer writer = new OutputStreamWriter(new FileOutputStream(plain), "UTF-8");
                    BufferedWriter out = new BufferedWriter(writer);
                    linetext = in.readLine();
                    while(linetext!=null){
                        linetext = linetext.replaceAll("[,.!?:;'0123456789 ]", "");
                        linetext = linetext.toLowerCase();
                        sB.append(linetext);
                        linetext = in.readLine();
                    }
                    all = sB.toString();
                    out.write(all);
                    in.close();
                    out.close();
                } catch (UnsupportedEncodingException e) {}
            } catch (FileNotFoundException e) {}
        } catch (IOException e) {}
    }

    public static void main(String[] args) throws FileNotFoundException {
        if(args.length == 0) {
            System.out.println("Niepoprawne wywolanie programu");
            System.exit(0);
        }
        String[] x = args;
        switch (x[0]) {
            case "-p":
                prepareFiles();
                break;

            case "-e":
                Vigenere.encrypt();
                break;

            case "-d":
                Vigenere.decrypt();
                break;

            case "-k":
                Vigenere.crypt();
                break;
            default :
                System.out.println("Niepoprawny argument!");
                break;
        }
    }
}

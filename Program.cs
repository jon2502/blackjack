
bool selectingPlayers = true;
int playerCount = 0;

while (selectingPlayers) {
    Console.WriteLine("Hello how may players are you: from 1 - 7");
    string playeroutput = Console.ReadLine() ?? "";
    try {
            playerCount = Int32.Parse(playeroutput);
        Console.WriteLine(playerCount);
        if( 0 > playerCount || playerCount < 4){
            selectingPlayers = false;
        } else {
            Console.WriteLine("Select a valid option: please try again");
        }
    } catch {
        Console.WriteLine("please select a number between 1 and 7");
    }
}
int i = 0;
while (playerCount >  i){
    Console.WriteLine(i);
}





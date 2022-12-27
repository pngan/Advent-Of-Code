<Query Kind="Program" />

void Main()
{
Console.WriteLine($"**** p1= {a()}");
Console.WriteLine($"**** p2= {b()}");
}

int b()
{
var input = Data.exampleb();
return input.Count();
}


int a()
{
var input = Data.testa();
for( int i = 13; i < input[0].Length; i++)
{
	string quad = input[0].Substring(i-13, 14);
	if (quad.Length == quad.Distinct().ToArray().Length)
		{
			Console.WriteLine(i+1);
			break;
		}
}
return input.Count();
}

/**** HINTS ****
data.Select(v => Convert.ToInt32(v)) // Convert to int
data.Split("\r\n\r\n");              // Split black lines
data.Select((x, i) => new { x, i })  // Project with index
data.GroupBy(x => x);                // Group by each unique
data.GroupBy(x => x).Select(g=> new {Key = g.Key.ToString(), Tally = g.Count()}).OrderByDescending(stat=>stat.Tally).ToList(); // Histogram


first.Intersect(second)               // Set AND
first.Except(second)                  // EXCLUDE
first.Union(second)                   // UNION (does not create duplicates)
first.Concat(second)                  // Concatenation (creates duplications) 
first.Distinct(second)                // UNIQUE

*/


int[] GetIntsFromLine(string regexPattern, string inputString)
{
Regex rg = new(regexPattern);
var matched = rg.Match(inputString);
return matched.Groups.Values.Skip(1).Select(v => Convert.ToInt32(v.Value)).ToArray();
}


public static class Data
{
	public static string[] testb() => tb.Split("\r\n");
	public const string tb =
@"REPLACE_ME"
	;

	public static string[] exampleb() => eb.Split("\r\n");
	public const string eb =
@"REPLACE_ME"
	;

	public static string[] testa() => ta.Split("\r\n");
	public const string ta =
"mgtgddtfdtffzvznvnrncrrbqqhlhhffzqqzpqqthhrhhfphfphhcppcddnwdnwwtmwttfvvthvvrrbvbmvmssrlslfslflppblllwrlrzlldwdllqblqbqbsscmsmwwffjpppnlnhllbblvbvsbbzvzrzzsmsjsddfpftfvtffgjjfzjfjqfqjfjsscvcccgttgtzgzmgmtmbbwzzjqzzdfzfmzmzfzwzvwvggqcqrrcwrcrzrccqcwwbgbqqwdqqzjzsjjwbjjssmmcfcbcddlhhtltmtlljffvjffhghmggmvvfgfqgfgppnpllmvmfvvzjjzrztztvvstsvvppqdpprjjmtmjtmjjdrdcrdccgsccnsccqsqzszqsqgqwggbhbllvclljrrlrqlljtjcjjlrlhrhjhjnnnpllwtwstttlnlqnlnmnqnpqpbqqbgbzzrhzrhzhrzhrzhzshhqvqgqgbbcqccqmcqccvgccrwrgwrgrdrhhbshbhwbhwbhhvthvttfrrqsstqssqmmpnpwpfpcffcdchhrsshrhggtcttmrrhvvjfvjvvclvllmqmvvhddrdjjhdhvhlvhlltlstltffbbqbwqbbbnsbnbwbssjwsjsfjjsjwwzttqzzsdspprlrblrltrrfrsfffwqwpwddddnqqtbtwwhwpwdwmmcrmrsmmwppjzpjpcpdpjpdpdqppmjjlqqjfqqhgqhhbddtccthhwjhhlfftvtppwzpwzpznpzpqpgqpgpnpdndnbnddqrrjdjwwdmmtnntvnnrhrfhfrfwfvwffmnfnlfldfdjjwgwqqwwsslrrvhhrqqsfqfllrmrqmrrbppwjppmlmggvppdhppspjjzljjrzzrlzrrlldllvlpvpfprfprrhdrdlllpqqfhqqhchzzzwpwjpjjgzzwqqtqdqbdbdgggbrgrzzznwwbvbnbpnbpprnrvvfvsscncrczcbchhjqhjjzrznndwnnvttmtthssgvvbvfvtvptpthhzggnjjhrjdqzjbtfpqdtwtmgnngqdzhdrfzqvcqggmcdbsdrdrmgqhmvfvdgbvrnlbhfsbpjhwgzfndqgcjdbpsffcslfcltsbclspdjhscqrncfrjrbjfzspccshtrdggjbhthrrhgnjvsptfnjvjvhhdjfbtfgpfgszhhbcvzplclrnsrpffpjhbthnfsfflqphhjjdpcfwzhfdpnsftrnfhrdhndlrnfrnvprtvnmgclzlrdjrzdcllvlwdlrcfbsgcbwcnbvjztzfsgcgqlmgcbsgwbbrmrcthfpvmbfvtbhqstccfntmphqpjwpbcdpnffqpszlnqdcqtfhvlvpgdpljvcschdtpvcswfzcbpqdhfjzzdjvgldspcvlnfnwffhjzdnbmjjtnrqlgnggsvdltnrpcfwqvphtsmrfzhflwjjbnpwlzhhmdnpqptgcjnrrgcnhwllqsbsjjvzmqsghlzvhdfbrnfhrqjswrpgcctsqvdwzgpqdssfmtgwvsznlbhsgppwdhhtjmscjfrjdgflwcrlbsfwrnvtnmcwpndhtttgqfvmvmfnwdrrvgmgdqlqvvlphwzgmwcphjvcfsqbbwttntmgvfmlmctggmtlwtmfsmczbgdvbsjstzgflnjplgrlhbbgldlchwmhclzbcwpqzlzbjzbplnvpbzjhmwfmrfnwlnsvpzhrgjdpqvnjtbfjfsvdqcfwdjftsmfqdrqllwlbnbmgtswrhbtbqlchznbgnphgntrtwbtmsjtphhqpbngwmmsdnsdqcctrsrzbrtpwtvhvqbrjldfldllpvspthdhdljfvjzcjsltwflscfqsrvzhgvzhqnnjwdwdtnsvgchzrnbzfscvsmrmqsqjmrjjdhtspbzpqtqqbfbzrddwqzwpqjbpbbbghlwmzhqvqdwwwwvltvvcpgzlwzvmqzfcgnjpjnpgsccvzpnzjwwnnjrcpbvwljfrjqzwsrvdmqwwfpldqcdwlchvggclmwnbhlrlzvsrtrqmzchqfqfhqfjgqsfvclnchdnnvdbqpcddnldggwrpbgrwwtssfndhrhnwtqmgrwpggntlqmfgbzjhwwsclvfmwgzzfrsccdfddntnlldpnwzhnzlssnnfbvjjhnrvclmphgfpvnwjzznbvgqnpljcrjpndgrlbdzsbfrrrfztbqcbphlppwcvhmrrmtrlvfjcddtznlmflrpsclgjpqczwrptfsccmdpzfvwnfsvshcnzrjrmstrslhgtrsmgplvcwptfqrgzgwhvtvrqlrjpcbztgtfwpnzqpmctvpdlgrtzzlsmgnftqvtvcmndspjqbdnmrttwhdrncsntntmrwjrqstdrptnhbqgtlqsdqfmbjtvgstndlvndqqsbqvcghwwjdzpszrsfpdzvnmnbzngczndtwtmprbzjdzbthslttzwwfptbphqwczsrqcbcbqnhbtcpjpbcqpjgjmhmfnggcbvctslpmqrpqzbcfrcgzmzpbpwzsjlrmpfzhgnnbqfrbslrfsthgtmsdfhzgdmjwwsgcdptssmbvffhlmfvwnmbpnzbvpsvnwsvsgrcmhpclwsbvtfqstnpzvgmgfcrmjhbccwcptssjhbfmzsqljjcrnnszvffzfwgcpnqrtjnqdltwnbglwlwpschvqwfdztvcwsqtwmgwccgsqbsvlwdhlnqphwtcmdpvvrqfwmlbptbvghvjntqbcsqjspwnmvdqcfbqzqchhhwqgdcmdhfvtzprscpshpbmzhwsznlpvzrwvmhtqsclzffgnvvrfbzmvqmnrrzjbmhdbspjprrmflgrwhnhcqpczchpnrnfjgdlnlrnzwnvjpmzgpfzspwmfnwcrrdczdhtnscmwqwqbcrdrsndpwbdvpgpbpsfzbmvjlsrdcgnwgrvmjnzlpnwtcrmnfcqgmlnhqbwlrnzlbdrnzfhnqddsfmnhnrrrdjgqprmgvrnhzrlccjthhfzdbltgrbrjpmbhvgrlwngdlfsljhfvwhvpmltdfnzwzcgzdpppnzcnpjttdgpzzqppnfzlmhrngbmcmshtgzbjllwstdbnmmwlrlllgfgshvcsjbpnggzrvvmvdqhjhvhmmpvrdqbrfpdtcdbqrvwhdrtqgftnwwzrcgzwmwjmdgmfswqwlgmvmvhscjmzshtbzmfmbqtbsjppzbczwcqpqhhqdggcntdchjgwsvfnzfqdzvhpnwbjhbqnldzbzmctcdqgjsmbqdzmmtjzvqzdqzsfpmncdmqlnpsrwcznbtzqtbcwwdqjftcdmmwdjdnwvpchffsmqmmwvqfgcnfhbjsttwnwppssmvrrhrbqwsncpfnbfggdqjwbgtvgtwsmlqbwzlghnzhjwphswjtbtptmhlzhvvrwqqcgwnmcqtcjlndwgjrpschhhsmrvvwtrjplwrtswhrjlgjhzgzrjhsbrjhtgnmfdvbjlntcrphsnmdcjzgwtvgldrfpcfgpzlgsfthdmpbnhmlsbnbqzpqvzzmvswbbnbtzvbsznqdgqlbbwzhjrzndltfgswtszsmmrhrcrcrcpgtqfcrmjrtflsbcbbmrsrfgnsrmbrpcvfpmqtmbrbbqtzrjntnvbvwjwqmwmcvmzccmwcnhrfpgghlqczcfszfhqgrdnfpnrrzpzbnjqjtvbglvqlhpstpzzcwrdgfhghqtsgzgsmgnpgvbsvsjtnwbvtqpcfdvhnjjvwjwglplthmghrwpmsgbdbfpvqsmsdvjgchlnlnczlzczqmjsnpgrgqgndwzdtlmmgzjpqvbqmcmhnhpqvpjjsftctwsrfmhrlctrvhczjbfsvqnshmchdsrmlrlqdnfsvhlblwghsdnrtwnpdtqgczmghqcmfzvsgqvrngjvbjsvnpzvpsplhvndvqpjjrtmrqscjrhvdmqcgwjmrgsdmgswgnbpdtgvvbrzrcwtvvwhpmcqwdtsmwwfgdpdrjsbvtbdvbhwftqznpssnsnjnclblslfgz"
	;

	public static string[] examplea() => ea.Split("\r\n");
	public const string ea =
"zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw"
	;
}








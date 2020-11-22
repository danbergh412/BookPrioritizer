import { UpdateBookVM } from "../models/updateBookVM.model";

export class ParseGoodReadsShelf
{
    public parseStr: string;
    constructor(parseStr: string){
        this.parseStr = parseStr;
    }
    parseLine(lineStr: string){
        var bookArray = lineStr
                .replace(/ *\([^)]*\) */g, "")
                .split('Rate this book\n1 of 5 stars2 of 5 stars3 of 5 stars4 of 5 stars5 of 5 stars')
                .join('')
                .split('\n')
                .filter(b => b.trim().length);
            
            if (bookArray.length == 0){
                return null;
            }
    
            var titleLine = bookArray[0].trim();
            
            titleLine = 
                titleLine
                .substring(0, titleLine.length/2);
    
            var authorRow = bookArray[1];
            var author = authorRow
                .substring(3, authorRow.length)
                .trim();
    
            var authorParts = author.split(' ');
    
            var numbers = bookArray[2]
                .match(/[0-9,\.]+/g);
            var rating = Number(numbers[0]);
            var popularity = Number(numbers[1].split(",").join(""));
            var year = Number(numbers[2]);
    
            var saveBook: UpdateBookVM = {
                bookId: undefined,
                name: titleLine,
                goodReadsRating: rating,
                goodReadsVotes: popularity,
                year: year,
                amazonRating: undefined,
                amazonVotes: undefined,
                statusId: 3,
                authorFirstName: authorParts[0],
                authorLastName: authorParts[authorParts.length - 1]
            };
            return saveBook;
    }
    parse(): UpdateBookVM[] {
        var tableArray: UpdateBookVM[] = [];
    
        var booksLines = this.parseStr.split("Want to Read");
        
        for(var line of booksLines){
            var book = this.parseLine(line);

            if (book){
                tableArray.push(book);
            }
        }
    
        return tableArray;
    }
}
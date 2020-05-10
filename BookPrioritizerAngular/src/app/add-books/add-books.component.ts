import { Component, OnInit } from '@angular/core';
import { BooksService } from '../books.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-books',
  templateUrl: './add-books.component.html',
  styleUrls: ['./add-books.component.css']
})
export class AddBooksComponent implements OnInit {

  parseText: string;
  showLoadingIndicator: boolean = false;

  constructor(private _router: Router, private _service: BooksService) { 

  }
  saveBooks(){
    var books = this.parseTableStr(this.parseText);
    
    this.showLoadingIndicator = true;
    this._service.saveBooks(books)
    .subscribe(() => {
        this.showLoadingIndicator = false;
        this.parseText = "";
        alert("Done!");
    })
  }

  parseTableStr(str: string): Book[] {
    var tableArray: Book[] = [];

    var books = str.split("Want to Read");
    
    for(var book of books){

        var bookArray = book
            .replace(/ *\([^)]*\) */g, "")
            .split('Rate this book\n1 of 5 stars2 of 5 stars3 of 5 stars4 of 5 stars5 of 5 stars')
            .join('')
            .split('\n')
            .filter(b => b.trim().length);
        
        if (bookArray.length == 0){
            continue;
        }

        var titleLine = bookArray[0].trim();
        
        titleLine = 
            titleLine
            .substring(0, titleLine.length/2);

        var authorRow = bookArray[1];
        var author = authorRow
            .substring(3, authorRow.length)
            .trim();

        var numbers = bookArray[2]
            .match(/[0-9,\.]+/g);
        var rating = Number(numbers[0]);
        var popularity = Number(numbers[1].split(",").join(""));

        var saveBook: Book = {
            authors: author, 
            title: titleLine, 
            rating: rating, 
            voters: popularity, 
            id: undefined,
            relevant: undefined,
            created: undefined,
            ratingPercentile: undefined,
            votersPercentile: undefined,
            score: undefined
        };
        tableArray.push(saveBook);
    }

    return tableArray;
}

  ngOnInit() {
  }

}

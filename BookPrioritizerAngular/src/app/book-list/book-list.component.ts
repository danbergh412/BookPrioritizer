import { Component, OnInit } from '@angular/core';
import { BooksService } from '../books.service';

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.css']
})
export class BookListComponent implements OnInit {
  allBooks: Book[];
  books: Book[];
  ratingWeight: number = 1;
  votersWeight: number = 2;
  showExcluded: boolean = false;
  selectedSort: string = "Score";
  sort: Array<string> = [
    "Score", 
    "Title",
    "Created"
  ]

  constructor(private _bookService: BooksService) { }

  toggleShowExcluded(){
    this.showExcluded = !this.showExcluded;
    this.refreshBooks();
  }


  ngOnInit() {
    this._bookService.getBooks()
    .subscribe((books: Book[]) => {
      this.allBooks = books;
      
      this.refreshBooks();
    })
  }
  refreshBooks() {
    this.books = this.getFilteredBooks();
    this.sortBooks();
  }
  toggleRelevant(book: Book){
    book.relevant = !book.relevant;
    this._bookService.editBook(book)
    .subscribe(() => {
      this.refreshBooks();
    })
  }

  getFilteredBooks(){
    var books = this.allBooks.filter((book: Book) => {
      return (this.showExcluded || book.relevant)? true: false;
    });
    var allRatings = books.map((book: Book) => book.rating);
    var allVoters = books.map((book: Book) => book.voters);

    for(var book of books){
      book.ratingPercentile = this.percentRank(allRatings, book.rating) *100;
      book.votersPercentile = this.percentRank(allVoters, book.voters) * 100;
      book.score = ((book.ratingPercentile * this.ratingWeight ) + (book.votersPercentile * this.votersWeight )) / (this.ratingWeight + this.votersWeight);
    }

    return books;
  }

  sortBooks(){
    console.log(this.selectedSort)
    this.books.sort((book1: Book, book2: Book) => {
          if (this.selectedSort == "Score"){
            return book2.score > book1.score? 1: -1;
          }
          else if (this.selectedSort == "Title"){
            return book2.title > book1.title? -1: 1;
          }
          else if (this.selectedSort == "Created"){
            return book2.created > book1.created? -1: 1;
          }
        });
  }

  percentRank(array: number[], value: number) {
      if (!array.length){
        return null;
      }
      var larger = array.filter((v: number) => {
        return v > value;
      })
      return (array.length - larger.length) / array.length;
  }


}

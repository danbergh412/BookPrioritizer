import { Component, OnInit } from '@angular/core';
import { BooksService } from '../services/books.service';
import { Book } from '../models/Book.model';
import { BookMapper } from '../mappers/book.mapper';
import { CalculateScore } from '../helpers/calculateScore';
import { FormGroup, FormControl } from '@angular/forms';

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
    var updateStatus = BookMapper.toggleHiddenVM(book);
    this._bookService.updateStatus(updateStatus)
    .subscribe(() => {
      this.refreshBooks();
    })
  }

  getFilteredBooks(){
    var books = this.allBooks.filter((book: Book) => {
      return (this.showExcluded || !book.hidden)? true: false;
    });
    
    var scoreCalc = new CalculateScore(books, this.ratingWeight, this.votersWeight);

    return scoreCalc.calculate();
  }

  sortBooks(){
    console.log(this.selectedSort)
    this.books.sort((book1: Book, book2: Book) => {
          if (this.selectedSort == "Score"){
            return book2.score > book1.score? 1: -1;
          }
          else if (this.selectedSort == "Title"){
            return book2.name > book1.name? -1: 1;
          }
          else if (this.selectedSort == "Created"){
            return book2.dateAdded > book1.dateAdded? -1: 1;
          }
        });
  }
}

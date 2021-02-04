from .models import book, author
from django.shortcuts import render, redirect



def index(request):
    context = {
      'all_books': book.objects.all(),
      'all_authors': author.objects.all(),
    }
    return render(request, "index.html", context)

def authors(request):
    context = {
      'all_books': book.objects.all(),
      'all_authors': author.objects.all(),
    }
    return render(request, "authors.html", context)

def add_book(request):
  title_from_form = request.POST['title']
  desc_from_form = request.POST['desc']
  book.objects.create(title = title_from_form, desc = desc_from_form)
  return redirect('/')

def add_author(request):
  first_name_from_form = request.POST['first_name']
  last_name_from_form = request.POST['last_name']
  notes_from_form = request.POST['notes']
  author.objects.create(first_name = first_name_from_form, last_name = last_name_from_form, notes= notes_from_form)
  return redirect('/authors')

def display_author(request, id_number):
  id_number = int(id_number)
  requested_author = author.objects.get(id=id_number)
  requested_books = author.objects.get(id=id_number).books.all()
  context = {
    'all_books': book.objects.all(),
    'requested': requested_author,
    'all_authors': author.objects.all(),
    'book_authors': requested_books

  }
  return render(request, "display_author.html", context)


def display_book(request, id_number):
  id_number = int(id_number)
  requested_book = book.objects.get(id=id_number)
  requested_authors = book.objects.get(id=id_number).authors.all()
  context = {
    'all_books': book.objects.all(),
    'requested': requested_book,
    'all_authors': author.objects.all(),
    'book_authors': requested_authors

  }
  return render(request, "display_book.html", context)  

def update_book(request):
  form_author_id = request.POST.get('author_id')
  form_book_id = request.POST.get('book_id')
  this_book = book.objects.get(id=form_book_id)
  person = author.objects.get(id=form_author_id)
  person.books.add(this_book)

  return redirect('/')


def update_author(request):
  form_author_id = request.POST['author_id']
  form_book_id = request.POST['book_id']
  this_book = book.objects.get(id=form_book_id)
  person = author.objects.get(id=form_author_id)
  person.books.add(this_book)
  return redirect('/authors')






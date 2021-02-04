from django.urls import path, include
from django.conf.urls import url
from . import views

urlpatterns = [
    path('', views.index),
    path('display_book/<int:id_number>/', views.display_book),
    path('display_author/<int:id_number>/', views.display_author),
    path('add_book/', views.add_book),
    path('add_author/', views.add_author),
    path('authors/', views.authors),
    path('update_author/', views.update_author),
    path('update_book/', views.update_book),

]

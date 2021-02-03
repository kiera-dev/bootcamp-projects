from django.urls import path, include
from django.conf.urls import url
from . import views

urlpatterns = [
    path('', views.index),
    path('show_info/<int:id_number>/', views.show_info),
    path('show_edit/', views.show_edit),
    path('add_show/', views.add_show),
    path('delete/<int:id_number>/', views.delete),
    path('edit/<int:id_number>/', views.edit),
    path('shows/', views.shows),
    path('add/', views.add),

]

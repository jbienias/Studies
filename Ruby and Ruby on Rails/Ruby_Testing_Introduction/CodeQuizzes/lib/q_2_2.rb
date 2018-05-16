class Array
  def sort_by_length
    self.sort {|x, y| x.length <=> y.length}
    #self.sort{|x,y| x.length - y.length} works too.
  end
end

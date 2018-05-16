require 'q_2_3'

describe "#mean" do
  it "calculates average" do
    arr = [1, 2, 3, 4]
    expect(arr.mean).to eq 2.5
  end
end
